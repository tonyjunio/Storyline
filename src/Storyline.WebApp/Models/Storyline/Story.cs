namespace Storyline.WebApp.Models.Storyline;

public class Story : BaseInfo
{
    public string ShortCode { get; set; } = string.Empty;
    public string LogoImage { get; set; } = string.Empty;
    public IEnumerable<StoryEvent> StoryEvents { get; set; } = [];
}