using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Storyline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Storyline.WebApp.Pages
{
    public class IndexBase : ComponentBase
    {

        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected IWebHostEnvironment HostEnv { get; set; }

        protected IEnumerable<StoryEvent> StoryEvents { get; set; }
        protected string Data { get; set; }
        protected string ErrMsg { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LoadDataAsync();
        }

        protected async Task LoadDataAsync()
        {
            try
            {
                this.StoryEvents = await Http.GetFromJsonAsync<IEnumerable<StoryEvent>>("jsondata/storyline-residentevil.json");

            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message + " | " + ex.StackTrace;
            }
        }

        protected void GenerateText()
        {

            try
            {
                this.Data = Guid.NewGuid().ToString().Replace("-", "");


                string path = Path.Combine(HostEnv.WebRootPath, $"{this.Data}.txt");
                File.WriteAllText(path, this.Data, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                this.Data = ex.Message + " | " + ex.StackTrace;
            }
        }

        protected async Task AddEventAsync(StoryEvent storyEvent)
        {
            if (storyEvent != null)
            {
                var storyEvents = await Http.GetFromJsonAsync<List<StoryEvent>>("jsondata/storyline-residentevil.json");
                if (storyEvents == null)
                {
                    storyEvents = new List<StoryEvent>();
                }

                storyEvent.EventId = (string.IsNullOrWhiteSpace(storyEvent.EventId) ? Guid.NewGuid().ToString().Replace("-", "") : storyEvent.EventId);
                storyEvents.Add(storyEvent);

                string jsonData = JsonSerializer.Serialize(storyEvents);


                string path = Path.Combine(HostEnv.WebRootPath, $"{this.Data}.txt");
                File.WriteAllText(path, this.Data, Encoding.UTF8);
            }
        }
    }
}
