namespace Storyline.WebApp.Models.Storyline;

public class Story : BaseInfo
{
    public string LogoImage { get; set; }
    public IEnumerable<StoryEvent> StoryEvents { get; set; }
}