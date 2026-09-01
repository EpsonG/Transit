namespace BlazorApp1.Services
{
    /// <summary>
    /// Service simple pour gérer le rôle de l'utilisateur courant dans l'application.
    /// Il est enregistré en tant que Singleton, donc l'état est partagé à travers l'application.
    /// ATTENTION : Ce service est une simulation et n'est pas sécurisé pour la production.
    /// </summary>
    public class RoleService
    {
        // Stocke le rôle actuel. Initialisé à "User" par défaut.
        public string CurrentRole { get; private set; } = "User";

        /// <summary>
        /// Définit le rôle de l'utilisateur.
        /// </summary>
        /// <param name="role">Le nouveau rôle ("Admin" ou "User").</param>
        public void SetRole(string role)
        {
            CurrentRole = role;
        }

        // Propriétés utilitaires pour vérifier rapidement le rôle.
        public bool IsAdmin => CurrentRole == "Admin";
        public bool IsUser => CurrentRole == "User";
    }
}
