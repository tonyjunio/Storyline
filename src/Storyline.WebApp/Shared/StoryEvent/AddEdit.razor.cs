using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Storyline.WebApp.Models.Storyline;
using Storyline.WebApp.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Storyline.WebApp.Shared.StoryEvent
{
    public class AddEditBase : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IWebHostEnvironment HostEnv { get; set; }

        [Inject]
        protected NavigationManager Navigator { get; set; }

        [Inject]
        protected IStorylineService StorylineService { get; set; }

        [Parameter]
        public Models.Storyline.StoryEvent SelectedStoryEvent { get; set; }

        [CascadingParameter(Name = "RefEventsDisplay")]
        public Shared.StoryEvent.Display RefEventsDisplay { get; set; }

        protected Models.Storyline.StoryEvent FormData;

        protected string SaveError { get; set; }

        protected ViewState VState { get; set; } = AddEditBase.ViewState.Create;

        protected override void OnInitialized()
        {
            this.Http.BaseAddress = new Uri(Navigator.BaseUri);
            this.FormData = new();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                MapValuesToForm();
            }
        }

        protected void MapValuesToForm()
        {
            if (this.SelectedStoryEvent != null && this.SelectedStoryEvent.Id != null)
            {
                this.FormData = this.SelectedStoryEvent;

                if (!string.IsNullOrWhiteSpace(this.FormData.Id))
                {
                    this.VState = ViewState.Update;
                }

                this.StateHasChanged();
            }
            else
            {
                this.FormData = new();
                this.VState = ViewState.Create;
                this.StateHasChanged();
            }
        }

        protected async Task SubmitAddEditFormAsync()
        {
            if (this.FormData != null)
            {
                try
                {
                    var story = await this.StorylineService.LoadStoryAsync();
                    List<Models.Storyline.StoryEvent> storyEvents = story?.StoryEvents?.ToList() ?? null;
                    if (story is null)
                    {
                        // Create a new list if the data is empty.
                        story = new Story();
                        storyEvents = new List<Models.Storyline.StoryEvent>();
                    }

                    if (string.IsNullOrWhiteSpace(this.FormData.Id))
                    {
                        // Create new story event.
                        this.FormData.Id = Guid.NewGuid().ToString().Replace("-", "");

                        storyEvents.Add(this.FormData);
                    }
                    else
                    {
                        // Update story event.
                        Models.Storyline.StoryEvent entry = storyEvents.FirstOrDefault(x => x.Id == this.FormData.Id);
                        storyEvents.Remove(entry);

                        storyEvents.Add(this.FormData);
                    }

                    string jsonData = JsonSerializer.Serialize(story);
                    string path = Path.Combine(HostEnv.WebRootPath, @$"jsondata\storyline-residentevil.json");

                    // Force to a fixed local drive path, so published can also reflect its changes to a single point.
                    //  D:\[Development]\Projects\AerionX\Storyline\src\Storyline.WebApp\wwwroot\jsondata
                    path = @"D:\[Development]\Projects\AerionX\Storyline\src\Storyline.WebApp\wwwroot\jsondata\storyline-residentevil.json";
                    File.WriteAllText(path, jsonData, Encoding.UTF8);

                    // Try saving to released.
                    try
                    {
                        string publishedPath = @"D:\[Development]\Projects\AerionX\Storyline\src\Storyline.WebApp\bin\Release\net5.0\publish\wwwroot\jsondata\storyline-residentevil.json";
                        File.WriteAllText(publishedPath, jsonData, Encoding.UTF8);
                    }
                    catch (Exception)
                    {
                        // Don't sweat the error.
                    }

                    RefEventsDisplay.SetStoryEvents(storyEvents);
                }
                catch (Exception ex)
                {
                    this.SaveError = ex.Message;
                }
            }
        }

        public void SetSelected(Models.Storyline.StoryEvent storyEvent)
        {
            this.SelectedStoryEvent = storyEvent;
            MapValuesToForm();
        }

        public enum ViewState
        {
            Create,
            Update
        }
    }
}
