namespace Core.Models;

public class Statistique
{
    public int IdMatch { get; set; }
    public int IdJoueur { get; set; }
    public string? NomJoueur { get; set; }
    public int Buts { get; set; }
    public int PassesDecisives { get; set; }
    public int CartonsJaunes { get; set; }
    public int CartonsRouges { get; set; }
    public int? MinutesJouees { get; set; }
}