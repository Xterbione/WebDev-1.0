namespace WebDevProject.Models
{
    public class StripboekModel
    {
        public int StripboekId { get; set; }
        public int genre_id { get; set; }
        public int serie_id { get; set; }
        public string stripboektitel { get; set; }
        public int aantal_paginas { get; set; }
        public int volgnummer { get; set; }
        public SerieModel SerieModel { get; set;}
        public GenreModel GenreModel { get; set; }  
    }
}
