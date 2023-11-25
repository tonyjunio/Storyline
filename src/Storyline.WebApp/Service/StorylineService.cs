using Storyline.WebApp.Models.Storyline;

namespace Storyline.WebApp.Service;

public class StorylineService : IStorylineService
{
    private readonly HttpClient _httpClient;

    public StorylineService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Story> LoadStoryAsync()
    {
        Story story = null;

        try
        {
            DTO.Storyline.Story data = await _httpClient.GetFromJsonAsync<DTO.Storyline.Story>("jsondata/storyline-residentevil.json");
            if (data is not null)
            {
                IEnumerable<DTO.Storyline.StoryEvent> dataStoryEvents = data.StoryEvents;

                var characters = await LoadCharactersAsync();
                var organizations = await LoadOrganizationsAsync();
                var viruses = await LoadVirusesAsync();
                var media = await LoadMediaAsync();

                story = new Story()
                {
                    Id = data.Id,
                    Title = data.Title,
                    ShortCode = data.ShortCode,
                    LogoImage = data.LogoImage,
                    StoryEvents = dataStoryEvents?.Select(e => new StoryEvent()
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Summary = e.Summary,
                        Location = e.Location,
                        EventTimeStart = e.EventTimeStart,
                        EventTimeEnd = e.EventTimeEnd,
                        Wiki = e.Wiki,
                        Image = e.Image,
                        Characters = characters?.Where(c => (e.Characters)?.Contains(c.Id) ?? false),
                        Organizations = organizations?.Where(o => (e.Organizations)?.Contains(o.Id) ?? false),
                        Viruses = viruses?.Where(v => (e.Viruses)?.Contains(v.Id) ?? false),
                        Games = media?.Where(g => g.MediaType == StoryMediaType.Game && ((e.Games)?.Contains(g.Id) ?? false)),
                        Movies = media?.Where(m => m.MediaType == StoryMediaType.Movie && ((e.Movies)?.Contains(m.Id) ?? false))
                    })?.OrderBy(o => o.EventTimeStart)
                };
            }
        }
        catch (Exception ex)
        {
            string errMsg = ex.Message;
        }

        return story;
    }

    public async Task<IEnumerable<Character>> LoadCharactersAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<IEnumerable<Character>>("jsondata/storyline-residentevil-characters.json");
        return data;
    }

    public async Task<IEnumerable<Organization>> LoadOrganizationsAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<IEnumerable<Organization>>("jsondata/storyline-residentevil-organizations.json");
        return data;
    }

    public async Task<IEnumerable<Virus>> LoadVirusesAsync()
    {
        IEnumerable<Virus> viruses = null;

        IEnumerable<DTO.Storyline.Virus> data = await _httpClient.GetFromJsonAsync<IEnumerable<DTO.Storyline.Virus>>("jsondata/storyline-residentevil-viruses.json");
        if (data is not null)
        {
            var organizations = await LoadOrganizationsAsync();

            viruses = data.Select(x =>
            {
                Organization org = organizations?.FirstOrDefault(o => o.Id == x.DevelopedBy);

                return new Virus() {
                    Id = x.Id,
                    Name = x.Name,
                    Summary = x.Summary,
                    ItemType = x.ItemType,
                    Image = x.Image,
                    Wiki = x.Wiki,
                    DevelopedBy = org
                };
            });
        }

        return viruses;
    }

    public async Task<IEnumerable<Media>> LoadMediaAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<IEnumerable<Media>>("jsondata/storyline-residentevil-media.json");
        return data;
    }
}
