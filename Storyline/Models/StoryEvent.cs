using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyline.Models
{
    public class StoryEvent
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime EventTime { get; set; }
        public string BannerImage { get; set; }
        public IEnumerable<string> CharacterTags { get; set; }
        public IEnumerable<Organization> OrganizationTags { get; set; }
        public IEnumerable<Virus> VirusTags { get; set; }
    }

}
