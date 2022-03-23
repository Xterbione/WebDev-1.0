namespace WebDevProject.Models;

public class WerktAanModel
{
    public int creator_id { get; set; }
    public int druk_id { get; set; }
    public string rol { get; set; }
    
    public CreatorModel CreatorModel { get; set; }
    public DrukModel DrukModel { get; set; }
}