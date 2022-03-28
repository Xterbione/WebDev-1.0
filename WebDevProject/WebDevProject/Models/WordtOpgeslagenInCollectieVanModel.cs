using System.ComponentModel.DataAnnotations;

namespace WebDevProject.Models;

public class WordtOpgeslagenInCollectieVanModel
{
    [Required]
    public int druk_id { get; set; }
    
    [Required]
    public int gebruiker_id { get; set; }
    
    [Display(Name = "Staat")]
    public string staat { get; set; }
    
    [Display(Name = "Aankoop waarde")] 
    public double aankoop_waarde { get; set; }
    
    [Display(Name = "Aankoop locatie")]
    public string aankoop_locatie { get; set; }
    
    [Display(Name = "Beoordeling")]
    public int beoordeling { get; set; }
    
    [Display(Name = "Opslag plaats")]
    public string opslag_locatie { set; get; }
    
    
    public DrukModel? DrukModel { get; set; }

}