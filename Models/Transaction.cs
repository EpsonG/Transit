using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente une transaction financière, généralement pour l'achat d'un ticket.
    /// </summary>
    public class Transaction
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Identifiant unique de la transaction, généré par l'application.
        [Required]
        public string TransactionId { get; set; } = "";

        // ID du ticket qui a été acheté lors de cette transaction.
        [Required]
        public string TicketId { get; set; } = "";

        // ID de l'utilisateur qui a effectué la transaction.
        [Required]
        public string UserId { get; set; } = "";

        // Montant de la transaction.
        [Required]
        public double Amount { get; set; }

        // Méthode de paiement utilisée.
        public string PaymentMethod { get; set; } = ""; // Cash, Card, Mobile

        // Date et heure de la transaction.
        public DateTime Timestamp { get; set; }
    }
}
