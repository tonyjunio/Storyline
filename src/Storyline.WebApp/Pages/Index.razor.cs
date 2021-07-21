using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using Storyline.WebApp.Shared.StoryEvent;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Storyline.WebApp.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime JS { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IWebHostEnvironment HostEnv { get; set; }

        [Inject]
        protected NavigationManager Navigator { get; set; }

        protected IEnumerable<Models.StoryEvent> StoryEvents { get; set; }
        protected string ErrMsg { get; set; }

        public Shared.StoryEvent.AddEdit RefAddEditEventForm { get; set; }

        public Shared.StoryEvent.Display RefEventsDisplay { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Http.BaseAddress = new Uri(Navigator.BaseUri);
            await this.LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            try
            {
                this.StoryEvents = await Http.GetFromJsonAsync<IEnumerable<Models.StoryEvent>>("jsondata/storyline-residentevil.json");
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
}
