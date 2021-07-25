using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
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

        [Parameter]
        public Models.StoryEvent SelectedStoryEvent { get; set; }

        [CascadingParameter(Name = "RefEventsDisplay")]
        public Shared.StoryEvent.Display RefEventsDisplay { get; set; }

        protected Models.StoryEvent FormData;

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
            if (this.SelectedStoryEvent != null && this.SelectedStoryEvent.EventId != null)
            {
                this.FormData = this.SelectedStoryEvent;

                if (!string.IsNullOrWhiteSpace(this.FormData.EventId))
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
                    var storyEvents = await this.Http.GetFromJsonAsync<List<Models.StoryEvent>>("jsondata/storyline-residentevil.json");
                    if (storyEvents == null)
                    {
                        // Create a new list if the data is empty.
                        storyEvents = new List<Models.StoryEvent>();
                    }

                    if (string.IsNullOrEmpty(this.FormData.EventId))
                    {
                        // Create new story event.
                        this.FormData.EventId = Guid.NewGuid().ToString().Replace("-", "");

                        // TODO: Iterate form characters.
                        // this.FormData.CharacterTags = 

                        storyEvents.Add(this.FormData);
                    }
                    else
                    {
                        // Update story event.
                        Models.StoryEvent entry = storyEvents.FirstOrDefault(x => x.EventId == this.FormData.EventId);
                        entry = this.FormData;
                    }

                    string jsonData = JsonSerializer.Serialize(storyEvents);
                    string path = Path.Combine(HostEnv.WebRootPath, $"jsondata/storyline-residentevil.json");

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

        public void SetSelected(Models.StoryEvent storyEvent)
        {
            this.SelectedStoryEvent = storyEvent;
            MapValuesToForm();
        }

        //public class AddEditStoryEventForm
        //{
        //    public string Id { get; set; }
        //    public string Title { get; set; }
        //    public string Summary { get; set; }
        //    public DateTime EventTimeStart { get; set; }
        //    public DateTime? EventTimeEnd { get; set; }
        //    public string Characters { get; set; }
        //    public string Organizations { get; set; }
        //    public string Viruses { get; set; }
        //    public string Games { get; set; }
        //    public string Movies { get; set; }
        //    public string BannerImage { get; set; }
        //}

        public enum ViewState
        {
            Create,
            Update
        }
    }
}
