using System.ComponentModel.DataAnnotations;

namespace WebDevProject.Models;

public class WordtOpgeslagenInCollectieVanModel
{
    [Required]
    public int druk_id { get; set; }
    
    [Required]
    public int gebruiker_id { get; set; }
    
    public string staat { get; set; }
    public double aankoop_waarde { get; set; }
    public string aankoop_locatie { get; set; }
    public int beoordeling { get; set; }
    public string opslag_locatie { set; get; }
    
    public DrukModel? DrukModel { get; set; }

}