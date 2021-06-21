using System;

namespace PublicationWebApi.Model
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
