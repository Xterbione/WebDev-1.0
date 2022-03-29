using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class seriebeheer : PageModel
{
    public IEnumerable<SerieModel> Series { get; set; }
    private SerieRepo serieRepo = new SerieRepo();
    [BindProperty(SupportsGet = true)] public string serie { get; set;}
    [BindProperty(SupportsGet = true)] public string landvanoorsprng { get; set;}
    [BindProperty(SupportsGet = true)] public DateTime eerstepublicatie { get; set;}
    [BindProperty(SupportsGet = true)] public bool lopend { get; set;}



    public void OnGet()
    {
        Series = serieRepo.Get();
    }
    public IActionResult OnGetUpdate([FromForm] int serieId)
    {
        if (serieId !=0)
        {
            return Redirect("serieUpdate");
        }
        else
        {
            return Page();
        }
    }
    public void OnPostDelete([FromForm] int serieId)
    {
        serieRepo.DeleteSingle(serieId);
        Series = serieRepo.Get();
        TempData["AlertMessage"] = "Serie Deleted successfully...";

    }
    
    public void OnPostAdd([FromForm] string stringlopend)
    {
        if (stringlopend =="ja" || stringlopend=="Ja" || stringlopend=="yes" || stringlopend=="Yes")
        {
            lopend = true;
        }
        else 
        {
            lopend = false;
        }
        if (serie!= null && landvanoorsprng!=null && eerstepublicatie !=null && lopend!=null)
        {
            serieRepo.insert(serie, landvanoorsprng,eerstepublicatie,lopend);
            Series = serieRepo.Get();
            TempData["AlertMessage"] = "Serie Added successfully...";


        }
       
    }
    
    
    
}