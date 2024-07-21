using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Storyline.WebApp.Shared.StoryEvent;

public class DisplayBase : ComponentBase
{
    [Inject]
    protected IJSRuntime? JS { get; set; }

    [Parameter]
    public Models.Storyline.Story? Story { get; set; }

    [Parameter]
    public EventCallback<Models.Storyline.StoryEvent> OnStoryEventChange { get; set; }


    [CascadingParameter(Name = "RefAddEditForm")]
    public StoryEvent.AddEdit? RefAddEditForm { get; set; }

    protected Models.Storyline.StoryEvent? SelectedStoryEvent { get; set; } = null;

    protected override void OnInitialized()
    {
        this.SelectedStoryEvent ??= this.Story?.StoryEvents.FirstOrDefault();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this.SelectedStoryEvent ??= this.Story?.StoryEvents.FirstOrDefault();
            this.StateHasChanged();

            // Build the timeline summary.
            // JS.InvokeVoidAsync("RenderTimelineSummary");
        }

        // Dynamically set the height of the time selector section.
        if (JS is not null)
        {
            await JS.InvokeVoidAsync("SetTimeSelectorHeight");
        }
    }

    protected void EditStoryEvent(Models.Storyline.StoryEvent storyEvent)
    {
        this.RefAddEditForm?.SetSelected(storyEvent);

        if (this.JS is not null)
        {
            Task.Run(async () => await this.JS.InvokeVoidAsync("ToggleMainOffCanvas"));
        }
    }

    protected void ChangeStoryEvent(Models.Storyline.StoryEvent storyEvent)
    {
        this.SelectedStoryEvent = storyEvent;
        this.OnStoryEventChange.InvokeAsync(storyEvent);
    }

    public void SetStoryEvents(IEnumerable<Models.Storyline.StoryEvent> storyEvents)
    {
        if (this.Story is not null)
        {
            this.Story.StoryEvents = storyEvents;
        }

        this.StateHasChanged();
    }
}
