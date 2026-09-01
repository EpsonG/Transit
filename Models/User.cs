using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente un utilisateur de l'application.
    /// </summary>
    public class User
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Identifiant unique de l'utilisateur, généré par l'application.
        [Required]
        public string UserId { get; set; } = "";

        // Nom d'utilisateur, doit avoir au moins 2 caractères.
        [Required, MinLength(2)]
        public string Username { get; set; } = "";

        // Adresse e-mail de l'utilisateur, doit être un format valide.
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        // Le hachage du mot de passe de l'utilisateur. Ne jamais stocker le mot de passe en clair !
        [Required]
        public string PasswordHash { get; set; } = "";

        // Rôle de l'utilisateur, détermine ses permissions.
        // Valeurs possibles : "Admin", "Passenger", "Driver".
        [Required]
        public string Role { get; set; } = ""; // Admin, Passenger, Driver

        // Date et heure de création du compte utilisateur.
        public DateTime CreatedAt { get; set; }
    }
}
