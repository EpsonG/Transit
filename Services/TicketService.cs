using BlazorApp1.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer toutes les opérations liées aux tickets.
    /// </summary>
    public class TicketService
    {
        // Contexte de la base de données pour accéder à la collection 'Tickets'.
        private readonly MongoDbContext _context;

        /// <summary>
        /// Constructeur qui injecte le contexte de la base de données.
        /// </summary>
        public TicketService(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les tickets.
        /// </summary>
        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Récupère les tickets d'un utilisateur spécifique et met à jour leur statut s'ils sont expirés.
        /// </summary>
        public async Task<List<Ticket>> GetByUserAsync(string userId)
        {
            var list = await _context.Tickets.Find(t => t.UserId == userId).ToListAsync();

            // Mise à jour automatique des tickets expirés
            foreach (var t in list)
            {
                if (t.Status == "Activated" &&
                    t.ValidUntil.HasValue &&
                    DateTime.UtcNow > t.ValidUntil.Value)
                {
                    t.Status = "Expired";
                    await UpdateStatusAsync(t.Id!, "Expired");
                }
            }

            return list;
        }

        /// <summary>
        /// Récupère tous les tickets pour une ligne donnée.
        /// </summary>
        public async Task<List<Ticket>> GetByLineAsync(string lineCode)
        {
            return await _context.Tickets.Find(t => t.LineCode == lineCode).ToListAsync();
        }

        /// <summary>
        /// Récupère un ticket par son ID unique.
        /// </summary>
        public async Task<Ticket?> GetByIdAsync(string id)
        {
            // id = t.Id (ObjectId sous forme de string)
            return await _context.Tickets.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Crée un nouveau ticket.
        /// </summary>
        public async Task CreateAsync(Ticket ticket)
        {
            await _context.Tickets.InsertOneAsync(ticket);
        }

        /// <summary>
        /// Met à jour un ticket existant.
        /// </summary>
        public async Task UpdateAsync(Ticket ticket)
        {
            await _context.Tickets.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }

        /// <summary>
        /// Supprime un ticket par son ID.
        /// </summary>
        public async Task DeleteAsync(string id)
        {
            await _context.Tickets.DeleteOneAsync(t => t.Id == id);
        }

        /// <summary>
        /// Met à jour uniquement le statut d'un ticket.
        /// </summary>
        public async Task UpdateStatusAsync(string id, string newStatus)
        {
            await _context.Tickets.UpdateOneAsync(
                t => t.Id == id,
                Builders<Ticket>.Update.Set(t => t.Status, newStatus)
            );
        }

        /// <summary>
        /// Active un ticket, définit sa date d'activation et sa date d'expiration en fonction de son type.
        /// </summary>
        public async Task ActivateAsync(string id)
        {
            var ticket = await _context.Tickets.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (ticket == null) return;

            var now = DateTime.UtcNow;
            DateTime validUntil = now;

            // Calcule la date de fin de validité selon le type de ticket.
            switch (ticket.TicketType)
            {
                case "OneRide90":
                    validUntil = now.AddMinutes(90);
                    break;

                case "ThreeDay":
                    validUntil = now.AddDays(3);
                    break;

                case "SevenDay":
                    validUntil = now.AddDays(7);
                    break;

                case "Monthly":
                    // fin du mois courant
                    validUntil = new DateTime(now.Year, now.Month, 1).AddMonths(1);
                    break;
            }

            // Prépare la mise à jour pour la base de données.
            var update = Builders<Ticket>.Update
                .Set(t => t.Status, "Activated")
                .Set(t => t.ActivationDate, now)
                .Set(t => t.ValidUntil, validUntil);

            await _context.Tickets.UpdateOneAsync(t => t.Id == id, update);
        }

        /// <summary>
        /// Marque un ticket comme "Utilisé".
        /// </summary>
        public async Task SetUsedAsync(string id)
        {
            await UpdateStatusAsync(id, "Used");
        }

        /// <summary>
        /// Calcule le nombre de tickets vendus pour chaque ligne en utilisant une requête d'agrégation MongoDB.
        /// </summary>
        public async Task<List<LineStat>> GetTicketCountPerLineAsync()
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$LineCode" },
                    { "TicketsCount", new BsonDocument("$sum", 1) }
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "LineCode", "$_id" },
                    { "TicketsCount", "$TicketsCount" },
                    { "_id", 0 }
                })
            };

            var aggregate = await _context.Tickets.AggregateAsync<LineStat>(pipeline);
            return await aggregate.ToListAsync();
        }
    }
}
