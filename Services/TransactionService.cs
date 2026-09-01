using BlazorApp1.Models;
using MongoDB.Driver;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer les transactions financières (achats de tickets).
    /// </summary>
    public class TransactionService
    {
        // Contexte de la base de données pour accéder à la collection 'Transactions'.
        private readonly MongoDbContext _context;

        /// <summary>
        /// Constructeur qui injecte le contexte de la base de données.
        /// </summary>
        public TransactionService(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les transactions.
        /// </summary>
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Récupère toutes les transactions pour un utilisateur spécifique.
        /// </summary>
        public async Task<List<Transaction>> GetByUserAsync(string userId)
        {
            return await _context.Transactions.Find(t => t.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Crée une nouvelle transaction dans la base de données.
        /// </summary>
        public async Task CreateAsync(Transaction tx)
        {
            await _context.Transactions.InsertOneAsync(tx);
        }
    }
}
