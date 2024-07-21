using Storyline.WebApp.Models.Storyline;

namespace Storyline.WebApp.Service
{
    public interface IStorylineService
    {
        Task<IEnumerable<Character>> LoadCharactersAsync();
        Task<IEnumerable<Media>> LoadMediaAsync();
        Task<IEnumerable<Organization>> LoadOrganizationsAsync();
        Task<Story> LoadStoryAsync();
        Task<IEnumerable<Virus>> LoadVirusesAsync();
    }
}