using BlazorApp1.Models;
using MongoDB.Driver;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer les opérations CRUD pour les stations.
    /// </summary>
    public class StationService
    {
        // Contexte de la base de données pour accéder à la collection 'Stations'.
        private readonly MongoDbContext _context;

        /// <summary>
        /// Constructeur qui injecte le contexte de la base de données.
        /// </summary>
        public StationService(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les stations.
        /// </summary>
        public async Task<List<Station>> GetAllAsync()
        {
            return await _context.Stations.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Récupère toutes les stations appartenant à une ligne spécifique.
        /// </summary>
        public async Task<List<Station>> GetByLineAsync(string lineCode)
        {
            return await _context.Stations.Find(s => s.LineCode == lineCode).ToListAsync();
        }

        /// <summary>
        /// Récupère une station par son code unique.
        /// </summary>
        public async Task<Station?> GetByCodeAsync(string stationCode)
        {
            return await _context.Stations.Find(s => s.StationCode == stationCode).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Crée une nouvelle station.
        /// </summary>
        public async Task CreateAsync(Station station)
        {
            await _context.Stations.InsertOneAsync(station);
        }

        /// <summary>
        /// Met à jour une station existante.
        /// </summary>
        public async Task UpdateAsync(Station station)
        {
            await _context.Stations.ReplaceOneAsync(s => s.StationCode == station.StationCode, station);
        }

        /// <summary>
        /// Supprime une station par son code.
        /// </summary>
        public async Task DeleteAsync(string stationCode)
        {
            await _context.Stations.DeleteOneAsync(s => s.StationCode == stationCode);
        }
    }
}
