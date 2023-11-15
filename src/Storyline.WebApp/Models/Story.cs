namespace Storyline.WebApp.Models;

public class Story : BaseInfo
{
    public IEnumerable<StoryEvent> StoryEvents { get; set; }
}