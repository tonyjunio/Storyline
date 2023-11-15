namespace Storyline.WebApp.Models;

public class StoryEvent : BaseInfo
{
    public DateTime EventTimeStart { get; set; }
    public DateTime? EventTimeEnd { get; set; }
    public IEnumerable<Character> Characters { get; set; }
    public IEnumerable<Organization> Organizations { get; set; }
    public IEnumerable<Media> Games { get; set; }
    public IEnumerable<Media> Movies { get; set; }
    public IEnumerable<Virus> Viruses { get; set; }
}
