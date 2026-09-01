namespace BlazorApp1.Models;

/// <summary>
/// Modèle simple utilisé pour représenter les statistiques de tickets par ligne.
/// Ce n'est pas une collection MongoDB, mais un objet de résultat pour une agrégation.
/// </summary>
public class LineStat
{
    // Le code de la ligne (ex: "B12").
    public string LineCode { get; set; } = "";
    // Le nombre total de tickets pour cette ligne.
    public int TicketsCount { get; set; }
}