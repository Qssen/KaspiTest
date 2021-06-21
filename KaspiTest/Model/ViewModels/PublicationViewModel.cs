using System;
using System.Collections.Generic;

namespace KaspiTest.Model.ViewModels
{
    class PublicationViewModel
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<WordViewModel> Words { get; set; } = new List<WordViewModel>();
    }
}
