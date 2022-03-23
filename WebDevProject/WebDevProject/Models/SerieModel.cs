namespace WebDevProject.Models
{
    public class SerieModel
    {
        public int serieId { get; set; }
        public string serieTitel { get; set; }
        public string land_van_oorsprong { get; set; }
        public DateTime eerste_publicatie { get; set; }
        public int lopend { get; set; }
    }
}
