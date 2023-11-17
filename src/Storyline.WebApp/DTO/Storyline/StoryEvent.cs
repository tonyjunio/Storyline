namespace Storyline.WebApp.DTO.Storyline;

public class StoryEvent
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public DateTime EventTimeStart { get; set; }
    public DateTime? EventTimeEnd { get; set; }
    public string Image { get; set; }
    public string[] Characters { get; set; }
    public string[] Organizations { get; set; }
    public string[] Viruses { get; set; }
    public string[] Games { get; set; }
    public string[] Movies { get; set; }
}
