using BlazorApp1.Models;
using MongoDB.Driver;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer les opérations CRUD (Créer, Lire, Mettre à jour, Supprimer) pour les lignes.
    /// </summary>
    public class LineService
    {
        // Contexte de la base de données pour accéder à la collection 'Lines'.
        private readonly MongoDbContext _context;

        /// <summary>
        /// Constructeur qui injecte le contexte de la base de données.
        /// </summary>
        public LineService(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les lignes de la base de données.
        /// </summary>
        public async Task<List<Line>> GetAllAsync()
        {
            return await _context.Lines.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Récupère une ligne spécifique par son code.
        /// </summary>
        public async Task<Line?> GetByCodeAsync(string code)
        {
            return await _context.Lines.Find(l => l.LineCode == code).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Crée une nouvelle ligne dans la base de données.
        /// </summary>
        public async Task CreateAsync(Line line)
        {
            await _context.Lines.InsertOneAsync(line);
        }

        /// <summary>
        /// Met à jour une ligne existante.
        /// </summary>
        public async Task UpdateAsync(Line line)
        {
            await _context.Lines.ReplaceOneAsync(l => l.LineCode == line.LineCode, line);
        }

        /// <summary>
        /// Supprime une ligne par son code.
        /// </summary>
        public async Task DeleteAsync(string lineCode)
        {
            await _context.Lines.DeleteOneAsync(l => l.LineCode == lineCode);
        }
    }
}
