namespace Storyline.WebApp.DTO.Storyline;

public class StoryEvent
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime EventTimeStart { get; set; }
    public DateTime? EventTimeEnd { get; set; }
    public string Wiki { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string[] Characters { get; set; } = [];
    public string[] Organizations { get; set; } = [];
    public string[] Viruses { get; set; } = [];
    public string[] Games { get; set; } = [];
    public string[] Movies { get; set; } = [];
}
