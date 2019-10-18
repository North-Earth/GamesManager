using System;
using System.Collections.Generic;

namespace GamesManager.Api.Models
{
    public class Release
    {
        public int id { get; set; }

        public string tag_name { get; set; }

        public string name { get; set; }

        public bool prerelease { get; set; }

        public string published_at { get; set; }

        public List<Asset> assets { get; set; }
    }
}
