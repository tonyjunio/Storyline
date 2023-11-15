namespace Storyline.WebApp.Models;

public class Virus : Item
{
    public Organization DevelopedBy { get; set; }
    public DateTime DevelopedOn { get; set; }
}
