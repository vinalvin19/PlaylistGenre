using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistGenreMvc.Models
{
    public class SongModel
    {
        public SongModel()
        {
            Genres = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Singer { get; set; }
        public string Year { get; set; }
        public string Writer { get; set; }
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}