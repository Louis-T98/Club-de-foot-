namespace Core.Models;

public class Contrat
{
    public int IdContrat { get; set; }
    public int IdJoueur { get; set; }
    public string? NomJoueur { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public decimal? SalaireMensuel { get; set; }
}