using System;
using System.Collections.Generic;
using System.Text;
using GamesManager.Client.Models.Enums;
using MaterialDesignThemes.Wpf;

namespace GamesManager.Client.Models
{
    public class LibraryItemModel
    {
        public string Name { get; set; }

        public GameName GameName { get; set; }

        public PackIconKind Icon { get; set; }
    }
}
