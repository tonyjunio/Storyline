using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Storyline.WebApp.Models
{
    public partial class StoryEvent
    {
        public string EventId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime EventTimeStart { get; set; }
        public DateTime? EventTimeEnd { get; set; }
        public string BannerImage { get; set; }
        public List<string> CharacterTags { get; set; }
        public IEnumerable<string> OrganizationTags { get; set; }
        public IEnumerable<string> VirusTags { get; set; }
        public IEnumerable<string> GameTags { get; set; }
        public IEnumerable<string> MovieTags { get; set; }

        public class Character
        {
            public string Code { get; set; }
        }
    }

}
