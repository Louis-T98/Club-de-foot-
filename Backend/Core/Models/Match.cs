namespace Core.Models;

public class Match
{
    public int IdMatch { get; set; }
    public int? IdEquipe { get; set; }
    public string? Adversaire { get; set; }
    public DateTime? DateMatch { get; set; }
    public int? ScorePour { get; set; }
    public int? ScoreContre { get; set; }
}