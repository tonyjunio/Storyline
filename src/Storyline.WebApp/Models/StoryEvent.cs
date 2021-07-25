using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyline.WebApp.Models
{
    public class StoryEvent
    {
        public string EventId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime EventTime { get; set; }
        public string BannerImage { get; set; }
        public IEnumerable<string> CharacterTags { get; set; }
        public IEnumerable<string> OrganizationTags { get; set; }
        public IEnumerable<string> VirusTags { get; set; }
        public IEnumerable<string> GameTags { get; set; }
        public IEnumerable<string> MovieTags { get; set; }
    }

}
