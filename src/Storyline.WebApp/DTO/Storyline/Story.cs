namespace Storyline.WebApp.DTO.Storyline;

public class Story
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public string LogoImage { get; set; } = string.Empty;
    public IEnumerable<StoryEvent> StoryEvents { get; set; } = [];
}
