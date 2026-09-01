using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente une entrée de journal (log) pour tracer les actions dans l'application.
    /// </summary>
    public class Log
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Identifiant unique du log, généré par l'application.
        [Required]
        public string LogId { get; set; } = "";

        // ID de l'utilisateur qui a effectué l'action. Peut être "System" pour les actions automatiques.
        [Required]
        public string UserId { get; set; } = "";

        // Le type d'action effectuée (ex: "Login", "TicketPurchase", "Create", "Error").
        [Required]
        public string Action { get; set; } = ""; // Login, TicketPurchase...

        // Le type de l'entité sur laquelle l'action a été effectuée (ex: "Ticket", "User").
        public string EntityType { get; set; } = ""; // Ticket, Line, User

        // L'identifiant de l'entité spécifique concernée.
        public string EntityId { get; set; } = "";

        // Un message détaillé décrivant l'événement.
        public string Message { get; set; } = "";

        // La date et l'heure à laquelle l'événement s'est produit.
        public DateTime Timestamp { get; set; }
    }
}
