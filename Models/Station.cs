using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp1.Models
{
    /// <summary>
    /// Représente une station ou un arrêt sur une ligne de transport.
    /// </summary>
    public class Station
    {
        // Identifiant unique généré par MongoDB.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Code unique de la station.
        [Required]
        public string StationCode { get; set; } = "";

        // Nom de la station.
        [Required]
        public string Name { get; set; } = "";

        // Code de la ligne à laquelle cette station appartient.
        [Required]
        public string LineCode { get; set; } = "";

        // Coordonnée géographique : latitude.
        public double Latitude { get; set; }
        // Coordonnée géographique : longitude.
        public double Longitude { get; set; }

        // Zone tarifaire de la station (ex: "A", "B", "C").
        public string Zone { get; set; } = ""; // A, B, C
    }
}
