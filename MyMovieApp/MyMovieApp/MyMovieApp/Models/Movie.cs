using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace MyMovieApp.Models
{
    public class Movie
    {
        [PrimaryKey]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string Poster { get; set; }

        public string PosterPath
        {
            get { return $@"https://image.tmdb.org/t/p/w185{Poster}"; }
        }

        [JsonProperty("vote_average")]
        public float Rating { get; set; }

        [JsonProperty("overview")]
        public string Summary { get; set; }        
    }
}
