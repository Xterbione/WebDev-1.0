namespace WebDevProject.Models;

public class WordtOpgeslagenInCollectieVanModel
{
    public int druk_id { get; set; }
    public int gebruiker_id { get; set; }
    public string staat { get; set; }
    public double aankoop_waarde { get; set; }
    public string aankoop_locatie { get; set; }
    public int beoordeling { get; set; }
    public string opslag_locatie { set; get; }
    
    public DrukModel DrukModel { get; set; }

}