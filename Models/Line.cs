using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente une ligne de transport (bus, métro, etc.).
    /// </summary>
    public class Line
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Code unique de la ligne (ex: "B12", "M1").
        [Required]
        public string LineCode { get; set; } = "";

        // Nom complet de la ligne (ex: "Porte de Clignancourt - Mairie de Montrouge").
        [Required]
        public string Name { get; set; } = "";

        // Description supplémentaire de la ligne.
        public string Description { get; set; } = "";

        // Indique si la ligne est actuellement en service.
        public bool IsActive { get; set; }

        // Catégorie de la ligne (ex: Urbain, Express, Périphérique).
        public string Category { get; set; } = ""; // Urban, Express, Suburban

        // Liste des codes des stations desservies par cette ligne.
        public List<string> Stations { get; set; } = new(); // List of StationCode

        // Mappage explicite pour le champ "StationCodes" dans MongoDB.
        // Note : Il semble y avoir une redondance avec la propriété "Stations".
        // Il serait bon de n'en garder qu'une pour éviter la confusion.
        [BsonElement("StationCodes")]
        public List<string> StationCodes { get; set; } = new();
    }
}
