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
        public StoryEvent.Display RefEventsDisplay { get; set; }

        protected AddEditStoryEventForm AddEditForm;

        protected string SaveError { get; set; }

        protected ViewState VState { get; set; } = AddEditBase.ViewState.Create;

        protected override void OnInitialized()
        {
            this.Http.BaseAddress = new Uri(Navigator.BaseUri);
            this.AddEditForm = new();
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
                this.AddEditForm = new()
                {
                    Id = this.SelectedStoryEvent.EventId,
                    Title = this.SelectedStoryEvent.Title,
                    Summary = this.SelectedStoryEvent.Summary,
                    EventTime = this.SelectedStoryEvent.EventTime,
                    Characters = string.Join(',', this.SelectedStoryEvent.CharacterTags ?? new[] { "" }),
                    Organizations = string.Join(',', this.SelectedStoryEvent.OrganizationTags ?? new[] { "" }),
                    Viruses = string.Join(',', this.SelectedStoryEvent.VirusTags ?? new[] { "" }),
                    Games = string.Join(',', this.SelectedStoryEvent.GameTags ?? new[] { "" }),
                    Movies = string.Join(',', this.SelectedStoryEvent.MovieTags ?? new[] { "" }),
                    BannerImage = this.SelectedStoryEvent.BannerImage
                };

                if (!string.IsNullOrWhiteSpace(this.AddEditForm.Id))
                {
                    this.VState = ViewState.Update;
                }

                this.StateHasChanged();
            }
            else
            {
                this.AddEditForm = new();
                this.VState = ViewState.Create;
                this.StateHasChanged();
            }
        }

        protected async Task SubmitAddEditFormAsync()
        {
            if (this.AddEditForm != null)
            {
                try
                {
                    var storyEvents = await this.Http.GetFromJsonAsync<List<Models.StoryEvent>>("jsondata/storyline-residentevil.json");
                    if (storyEvents == null)
                    {
                        // Create a new list if the data is empty.
                        storyEvents = new List<Models.StoryEvent>();
                    }

                    if (string.IsNullOrEmpty(this.AddEditForm.Id))
                    {
                        // Create New
                        Models.StoryEvent newEntry = new Models.StoryEvent
                        {
                            EventId = Guid.NewGuid().ToString().Replace("-", ""),
                            Title = this.AddEditForm.Title,
                            Summary = this.AddEditForm.Summary,
                            EventTime = this.AddEditForm.EventTime,
                            CharacterTags = this.AddEditForm.Characters.Split(',', ';'),
                            OrganizationTags = this.AddEditForm.Organizations.Split(',', ';'),
                            VirusTags = this.AddEditForm.Viruses.Split(',', ';'),
                            GameTags = this.AddEditForm.Games.Split(',', ';'),
                            MovieTags = this.AddEditForm.Movies.Split(',', ';'),
                            BannerImage = this.AddEditForm.BannerImage
                        };

                        storyEvents.Add(newEntry);
                    }
                    else
                    {
                        Models.StoryEvent entry = storyEvents.FirstOrDefault(x => x.EventId == this.AddEditForm.Id);
                        entry.EventId = this.AddEditForm.Id;
                        entry.Title = this.AddEditForm.Title;
                        entry.Summary = this.AddEditForm.Summary;
                        entry.EventTime = this.AddEditForm.EventTime;
                        entry.CharacterTags = this.AddEditForm.Characters.Split(',', ';');
                        entry.OrganizationTags = this.AddEditForm.Organizations.Split(',', ';');
                        entry.VirusTags = this.AddEditForm.Viruses.Split(',', ';');
                        entry.GameTags = this.AddEditForm.Games.Split(',', ';');
                        entry.MovieTags = this.AddEditForm.Movies.Split(',', ';');
                        entry.BannerImage = this.AddEditForm.BannerImage;
                    }

                    string jsonData = JsonSerializer.Serialize(storyEvents);
                    string path = Path.Combine(HostEnv.WebRootPath, $"jsondata/storyline-residentevil.json");
                    File.WriteAllText(path, jsonData, Encoding.UTF8);

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

        public class AddEditStoryEventForm
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime EventTime { get; set; }
            public string Characters { get; set; }
            public string Organizations { get; set; }
            public string Viruses { get; set; }
            public string Games { get; set; }
            public string Movies { get; set; }
            public string BannerImage { get; set; }
        }

        public enum ViewState
        {
            Create,
            Update
        }
    }
}
