using System;
using System.Collections.Generic;
using System.Text;

namespace GamesManager.Client.Models
{
    public class NewsItemModel
    {
        public string Header { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleseDateView
        {
            get => ReleaseDate.ToString("D");
        }
    }
}
