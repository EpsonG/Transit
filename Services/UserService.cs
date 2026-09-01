using System.Text;
using BlazorApp1.Models;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Service pour gérer les utilisateurs : création, authentification, mise à jour, etc.
    /// </summary>
    public class UserService
    {
        private readonly MongoDbContext _context;
        private readonly LogService _logService; // Service pour enregistrer les actions.

        /// <summary>
        /// Constructeur qui injecte les services nécessaires.
        /// </summary>
        public UserService(MongoDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        /// <summary>
        /// Récupère tous les utilisateurs. Inclut une gestion d'erreurs avec logging.
        /// </summary>
        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                await _logService.LogAction("System", "Error", "User", "GetAll", $"Échec de la récupération des utilisateurs. Erreur: {ex.Message}");
                // En cas d'erreur, retourne une liste vide.
                return new List<User>();
            }
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            try
            {
                return await _context.Users.Find(u => u.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                await _logService.LogAction("System", "Error", "User", userId, $"Échec de la récupération de l'utilisateur {userId}. Erreur: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crée un nouvel utilisateur, hache son mot de passe et enregistre l'action.
        /// </summary>
        public async Task<bool> CreateAsync(User user, string password)
        {
            try
            {
                user.PasswordHash = ComputeHash(password); // Hache le mot de passe avant de le stocker.
                user.CreatedAt = DateTime.UtcNow; // Définit la date de création.
                user.UserId = Guid.NewGuid().ToString("N").Substring(0, 8); // Génère un ID utilisateur court et unique.

                await _context.Users.InsertOneAsync(user);

                await _logService.LogAction(user.UserId, "Create", "User", user.UserId,
                    $"User {user.Username} created successfully.");

                return true;
            }
            catch (Exception ex)
            {
                await _logService.LogAction("System", "Error", "User", user.UserId, $"Échec de la création de l'utilisateur {user.Username}. Erreur: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Met à jour les informations d'un utilisateur existant.
        /// </summary>
        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                var result = await _context.Users.ReplaceOneAsync(u => u.UserId == user.UserId, user);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    await _logService.LogAction(user.UserId, "Update", "User", user.UserId, $"User {user.Username} updated successfully.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await _logService.LogAction("System", "Error", "User", user.UserId, $"Échec de la mise à jour de l'utilisateur {user.Username}. Erreur: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Supprime un utilisateur par son ID.
        /// </summary>
        public async Task<bool> DeleteAsync(string userId)
        {
            try
            {
                var result = await _context.Users.DeleteOneAsync(u => u.UserId == userId);
                if (result.IsAcknowledged && result.DeletedCount > 0)
                {
                    await _logService.LogAction("System", "Delete", "User", userId, $"User {userId} deleted successfully.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await _logService.LogAction("System", "Error", "User", userId, $"Échec de la suppression de l'utilisateur {userId}. Erreur: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Authentifie un utilisateur en comparant le nom d'utilisateur/email et le mot de passe haché.
        /// </summary>
        public async Task<User?> AuthenticateAsync(string usernameOrEmail, string password)
        {
            var hash = ComputeHash(password);

            return await _context.Users
                .Find(u =>
                    (u.Username.ToLower() == usernameOrEmail.ToLower()
                    || u.Email.ToLower() == usernameOrEmail.ToLower())
                    && u.PasswordHash == hash)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Calcule le hachage SHA256 d'une chaîne de caractères (pour les mots de passe).
        /// </summary>
        private string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToHexString(sha.ComputeHash(bytes));
        }
    }
}
