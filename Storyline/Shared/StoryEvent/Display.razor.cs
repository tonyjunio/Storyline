using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Storyline.Shared.StoryEvent
{
    public class DisplayBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public IEnumerable<Models.StoryEvent> StoryEvents { get; set; }
    }
}
