namespace Core.Models;

public class Joueur
{
    public int IdJoueur { get; set; }
    public int? IdEquipe { get; set; }
    public int? IdPoste { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public DateTime? DateNaissance { get; set; }
    public string? Nationalite { get; set; }
    public int? NumeroMaillot { get; set; }
}