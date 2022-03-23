namespace WebDevProject.Models
{
    public class DrukModel
    {
        public int drukId { get; set; }
        public int stripboek_id { get; set; }
        public int druknummer { get; set; }
        public DateTime druk_datum { get; set; }
        public string uitvoering { get; set; }
        public int oplage { get; set; }
        public decimal waarde { get; set; }
        public string isbn { get; set; }
        public int uitgever_id { get; set; }

        public StripboekModel StripboekModel { get; set; }
        public UitgeverModel UitgeverModel { get; set; }
        public WerktAanModel WerktAanModel { set; get; }
        public CreatorModel CreatorModel { get; set; }
    }
}
