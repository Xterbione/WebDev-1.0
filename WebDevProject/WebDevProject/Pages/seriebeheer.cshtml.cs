using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class seriebeheer : PageModel
{
    public IEnumerable<SerieModel> Series { get; set; }
    private SerieRepo serieRepo = new SerieRepo();
    [BindProperty(SupportsGet = true)] public string serie { get; set; }
    public IActionResult OnGet()
    {

        if (HttpContext.Session.GetString("cockie") == null)
        {
            return new RedirectToPageResult("/Index");
           
        }
        
        // return new RedirectToPageResult("/Index");
        else
        { 
            Series = serieRepo.Get();
            
        }
        return Page();
    }
    
}