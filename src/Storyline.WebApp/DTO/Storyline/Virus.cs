namespace Storyline.WebApp.DTO.Storyline;

public class Virus
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Wiki { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string DevelopedBy { get; set; } = string.Empty;
    public DateTime? DevelopedOn { get; set; }
}
