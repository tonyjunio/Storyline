namespace Storyline.WebApp.Models;

public class Organization : BaseEntity
{
    public IEnumerable<Organization> Affiliations { get; set; }
}
