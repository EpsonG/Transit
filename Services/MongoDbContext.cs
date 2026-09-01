using BlazorApp1.Models;
using MongoDB.Driver;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Représente le contexte de la base de données MongoDB.
    /// Cette classe est responsable de la connexion à la base de données et de l'accès aux collections.
    /// </summary>
    public class MongoDbContext
    {
        // Propriété pour accéder à l'instance de la base de données.
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Constructeur qui initialise la connexion à la base de données MongoDB.
        /// </summary>
        /// <param name="config">Service de configuration pour récupérer la chaîne de connexion et le nom de la base.</param>
        public MongoDbContext(IConfiguration config)
        {
            // Crée un nouveau client MongoDB en utilisant la chaîne de connexion depuis la configuration.
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            // Récupère l'instance de la base de données en utilisant le nom depuis la configuration.
            Database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        }

        // Raccourcis pour accéder facilement aux différentes collections de la base de données.
        public IMongoCollection<User> Users => Database.GetCollection<User>("Users");                 // Collection des utilisateurs.
        public IMongoCollection<Line> Lines => Database.GetCollection<Line>("Lines");                 // Collection des lignes.
        public IMongoCollection<Station> Stations => Database.GetCollection<Station>("Stations");         // Collection des stations.
        public IMongoCollection<Ticket> Tickets => Database.GetCollection<Ticket>("Tickets");             // Collection des tickets.
        public IMongoCollection<Transaction> Transactions => Database.GetCollection<Transaction>("Transactions"); // Collection des transactions.
        public IMongoCollection<Log> Logs => Database.GetCollection<Log>("Logs");                     // Collection des logs.
    }
}
