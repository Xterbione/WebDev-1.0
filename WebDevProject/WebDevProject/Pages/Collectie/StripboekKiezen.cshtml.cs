using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class StripboekKiezen : PageModel
{
    public IEnumerable<StripboekModel> Stripboeken { get; set; }
    public DrukRepo stripboekrepo = new DrukRepo();
    
    
    
    public void OnGet()
    { 
        Stripboeken = new StripboekRepo().Get();
    }
    
}