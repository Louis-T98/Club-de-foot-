namespace Core.Models;

public class Club
{
    public int IdClub { get; set; }
    public string? Nom { get; set; }
    public string? Ville { get; set; }
    public string? Stade { get; set; }
    public DateTime? DateFondation { get; set; }
}