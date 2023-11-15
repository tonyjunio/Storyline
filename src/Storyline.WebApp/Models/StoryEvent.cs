namespace Storyline.WebApp.Models;

public class StoryEvent : BaseInfo
{
    public DateTime EventTimeStart { get; set; }
    public DateTime? EventTimeEnd { get; set; }
    public IEnumerable<Character> Characters { get; set; }
    public IEnumerable<string> Organizations { get; set; }
    public IEnumerable<string> Games { get; set; }
    public IEnumerable<string> Movies { get; set; }
    public IEnumerable<string> Viruses { get; set; }
}
