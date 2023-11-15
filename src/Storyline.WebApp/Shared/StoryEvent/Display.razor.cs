using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Storyline.WebApp.Shared.StoryEvent
{
    public class DisplayBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime JS { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public Models.Story Story { get; set; }

        protected Models.StoryEvent SelectedStoryEvent { get; set; } = null;


        [CascadingParameter(Name = "RefAddEditForm")]
        public StoryEvent.AddEdit RefAddEditForm { get; set; }

        protected override void OnInitialized()
        {
            var data = this.Story;
        }

        protected void EditStoryEvent(Models.StoryEvent storyEvent)
        {
            this.RefAddEditForm.SetSelected(storyEvent);

            Task.Run(async () => await this.JS.InvokeVoidAsync("ToggleMainOffCanvas"));
        }
        public void SetStoryEvents(IEnumerable<Models.StoryEvent> storyEvents)
        {
            this.Story.StoryEvents = storyEvents;
            this.StateHasChanged();
        }
    }
}
