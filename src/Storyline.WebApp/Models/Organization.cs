namespace Storyline.WebApp.Models;

public class Organization : Entity
{
    public IEnumerable<Organization> Affiliations { get; set; }
}
