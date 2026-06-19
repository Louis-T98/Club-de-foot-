namespace Core.Models;

public class Entrainement
{
    public int IdEntrainement { get; set; }
    public int? IdEquipe { get; set; }
    public string? NomEquipe { get; set; }
    public DateTime? DateEntrainement { get; set; }
    public int? DureeMinutes { get; set; }
    public string? Theme { get; set; }
}