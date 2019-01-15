using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApplication.Domain.Dto.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string ImdbID { get; set; }
    }
}
