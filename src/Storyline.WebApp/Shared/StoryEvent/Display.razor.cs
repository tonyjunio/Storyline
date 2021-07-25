using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
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
        public IEnumerable<Models.StoryEvent> StoryEvents { get; set; }

        [CascadingParameter(Name = "RefAddEditForm")]
        public AddEdit RefAddEditForm { get; set; }

        protected override void OnInitialized()
        {
            var data = this.StoryEvents;
        }

        protected void EditStoryEvent(Models.StoryEvent storyEvent)
        {
            this.RefAddEditForm.SetSelected(storyEvent);

            Task.Run(async () => await this.JS.InvokeVoidAsync("ToggleMainOffCanvas"));
        }
        public void SetStoryEvents(IEnumerable<Models.StoryEvent> storyEvents)
        {
            this.StoryEvents = storyEvents;
            this.StateHasChanged();
        }
    }
}
