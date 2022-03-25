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
    public void OnPostDelete([FromForm] string? hiddenSerie)
    {
        serieRepo.DeleteSingle(hiddenSerie);
        Series = serieRepo.Get();
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

        }
       
    }
    
    /*public void OnPostUpdate([FromForm] string? serie, [FromForm] int hiddenSerie)
    {
        if (hiddenSerie !=0 && serie != null) 
        {
            serieRepo.Update(serie,hiddenSerie);
            genres = serieRepo.Get();

        }
    }*/
    
}