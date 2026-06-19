namespace Core.Models;

public class Staff
{
    public int IdStaff { get; set; }
    public int? IdEquipe { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string? Role { get; set; }
}