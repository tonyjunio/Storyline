using Microsoft.AspNetCore.Components;
using Storyline.Models;
using System.Collections.Generic;

namespace Storyline.Shared
{
    public class StoryEventsBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public IEnumerable<StoryEvent> StoryEvents { get; set; }
    }
}
