using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyline.WebApp.Shared.StoryEvent
{
    public class DisplayBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public IEnumerable<Models.StoryEvent> StoryEvents { get; set; }

        protected override void OnInitialized()
        {
            var data = this.StoryEvents;
        }
    }
}
