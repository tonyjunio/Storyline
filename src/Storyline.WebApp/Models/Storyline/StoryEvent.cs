﻿namespace Storyline.WebApp.Models.Storyline;

public class StoryEvent : BaseInfo
{
    public string Location { get; set; } = string.Empty;
    public DateTime EventTimeStart { get; set; }
    public DateTime? EventTimeEnd { get; set; } = null;
    public IEnumerable<Character> Characters { get; set; } = [];
    public IEnumerable<Organization> Organizations { get; set; } = [];
    public IEnumerable<Media> Games { get; set; } = [];
    public IEnumerable<Media> Movies { get; set; } = [];
    public IEnumerable<Virus> Viruses { get; set; } = [];
}