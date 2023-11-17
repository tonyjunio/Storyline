namespace Storyline.WebApp.Models.Storyline;

public class Virus : Item
{
    public Organization DevelopedBy { get; set; }
    public DateTime DevelopedOn { get; set; }
}
