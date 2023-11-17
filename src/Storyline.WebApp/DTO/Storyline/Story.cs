namespace Storyline.WebApp.DTO.Storyline;

public class Story
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string LogoImage { get; set; }
    public IEnumerable<StoryEvent> StoryEvents { get; set; }
}
