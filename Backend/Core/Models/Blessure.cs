namespace Core.Models;

public class Blessure
{
    public int IdBlessure { get; set; }
    public int IdJoueur { get; set; }
    public string? NomJoueur { get; set; }
    public string? TypeBlessure { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
}