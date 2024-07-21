namespace Storyline.WebApp.Models.Storyline;

public class BaseInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Image {get; set; } = string.Empty;
    public string Wiki { get; set; } = string.Empty;
    public string[] Tags { get; set; } = [];
}
