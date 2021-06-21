using System;
using System.Collections.Generic;

namespace KaspiTest.Model.Domain
{
    class Publication
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
