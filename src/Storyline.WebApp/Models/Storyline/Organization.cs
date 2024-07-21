namespace Storyline.WebApp.Models.Storyline;

public class Organization : Entity
{
    public IEnumerable<Organization> Affiliations { get; set; } = [];
}
