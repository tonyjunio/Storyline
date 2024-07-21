using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Storyline.WebApp.Shared.StoryEvent;
using Storyline.WebApp.Service;

namespace Storyline.WebApp.Pages;

public class IndexBase : ComponentBase
{
    [Inject]
    protected IJSRuntime? JS { get; set; }

    [Inject]
    protected IWebHostEnvironment? HostEnv { get; set; }

    [Inject]
    protected IStorylineService? StorylineService { get; set; }

    protected Models.Storyline.Story Story { get; set; } = new();

    protected string ErrMsg { get; set; } = "";

    public AddEdit? RefAddEditEventForm { get; set; }

    public Display? RefEventsDisplay { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        try
        {
            if (StorylineService is not null)
            {
                this.Story = await StorylineService.LoadStoryAsync();
            }
        }
        catch (Exception ex)
        {
            ErrMsg = ex.Message + " | " + ex.StackTrace;
        }
    }

    /// <summary>
    /// TODO: When adding from Create modal.
    /// </summary>
    protected void CreateStoryEvent()
    {
        RefAddEditEventForm.SetSelected(new());

        if (JS is not null)
        {
            Task.Run(async () => await JS.InvokeVoidAsync("ToggleMainOffCanvas"));
        }
    }
}
