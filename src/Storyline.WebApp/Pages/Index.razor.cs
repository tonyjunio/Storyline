using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Storyline.WebApp.Shared.StoryEvent;
using Storyline.WebApp.Service;

namespace Storyline.WebApp.Pages;

public class IndexBase : ComponentBase
{
    [Inject]
    protected IJSRuntime JS { get; set; }

    [Inject]
    protected IWebHostEnvironment HostEnv { get; set; }

    [Inject]
    protected IStorylineService StorylineService { get; set; }

    protected Models.Storyline.Story Story { get; set; }

    protected string ErrMsg { get; set; }

    public AddEdit RefAddEditEventForm { get; set; }

    public Display RefEventsDisplay { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await this.LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        try
        {
            this.Story = await this.StorylineService.LoadStoryAsync();
        }
        catch (Exception ex)
        {
            ErrMsg = ex.Message + " | " + ex.StackTrace;
        }
    }

    protected void CreateStoryEvent()
    {
        this.RefAddEditEventForm.SetSelected(new());

        Task.Run(async () => await this.JS.InvokeVoidAsync("ToggleMainOffCanvas"));
    }
}
