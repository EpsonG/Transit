using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente un ticket de transport.
    /// </summary>
    public class Ticket
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Identifiant unique du ticket, généré par l'application.
        [Required]
        public string TicketId { get; set; } = Guid.NewGuid().ToString();

        // ID de l'utilisateur qui possède le ticket.
        [Required]
        public string UserId { get; set; } = "";

        // Code de la ligne pour laquelle le ticket est valide.
        [Required]
        public string LineCode { get; set; } = "";

        // Date et heure de l'achat du ticket.
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        // Prix du ticket.
        public double Price { get; set; }

        // Statut actuel du ticket.
        // Valeurs possibles : "Purchased" (Acheté), "Activated" (Activé), "Expired" (Expiré), "Used" (Utilisé).
        public string Status { get; set; } = "Purchased";

        // Type de ticket, qui détermine sa durée de validité.
        // Valeurs possibles : "OneRide90", "ThreeDay", "SevenDay", "Monthly".
        public string TicketType { get; set; } = "OneRide90";

        // Date et heure de la première activation du ticket. Null si jamais activé.
        public DateTime? ActivationDate { get; set; }

        // Date et heure d'expiration du ticket après son activation. Null si jamais activé.
        public DateTime? ValidUntil { get; set; }
    }
}
