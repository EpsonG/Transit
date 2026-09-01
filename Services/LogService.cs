using BlazorApp1.Models;
using MongoDB.Driver;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer la création et la lecture des logs (journaux d'événements).
    /// </summary>
    public class LogService
    {
        // Contexte de la base de données pour accéder à la collection 'Logs'.
        private readonly MongoDbContext _context;

        /// <summary>
        /// Constructeur qui injecte le contexte de la base de données.
        /// </summary>
        public LogService(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les logs, triés par date décroissante.
        /// </summary>
        public async Task<List<Log>> GetAllAsync()
        {
            return await _context.Logs.Find(_ => true).SortByDescending(l => l.Timestamp).ToListAsync();
        }

        /// <summary>
        /// Crée une nouvelle entrée de log dans la base de données.
        /// </summary>
        public async Task CreateAsync(Log log)
        {
            await _context.Logs.InsertOneAsync(log);
        }

        /// <summary>
        /// Méthode utilitaire pour créer et enregistrer un log standardisé.
        /// </summary>
        /// <param name="userId">ID de l'utilisateur effectuant l'action.</param>
        /// <param name="action">Type d'action (ex: "Create", "Update", "Error").</param>
        /// <param name="entityType">Type de l'entité concernée (ex: "User", "Ticket").</param>
        /// <param name="entityId">ID de l'entité concernée.</param>
        /// <param name="message">Message descriptif du log.</param>
        public async Task LogAction(string userId, string action, string entityType, string entityId, string message)
        {
            var log = new Log
            {
                LogId = Guid.NewGuid().ToString(), // Génère un ID unique pour le log.
                UserId = userId,                   // Qui a fait l'action.
                Action = action,                   // Quelle action.
                EntityType = entityType,           // Sur quel type d'objet.
                EntityId = entityId,               // Sur quel objet spécifique.
                Message = message,                 // Description.
                Timestamp = DateTime.Now           // Quand l'action a eu lieu.
            };

            await _context.Logs.InsertOneAsync(log);
        }
    }
}
