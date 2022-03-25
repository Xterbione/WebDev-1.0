using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class serieUpdate : PageModel
{
    public IEnumerable<SerieModel> Series { get; set; }
    private SerieRepo serieRepo = new SerieRepo();
    [BindProperty(SupportsGet = true)] public bool lopend { get; set; }

    public void OnGet([FromRoute] int serieId)
    {
        Series = serieRepo.GetById(serieId);
    }

    public IActionResult OnPostUpdate(int serieId, string serie, string landvanoorsprong, string eerstepublcatie, string stringlopned)
    {
        if (stringlopned =="ja" || stringlopned=="Ja" || stringlopned=="yes" || stringlopned=="Yes")
        {
            lopend = true;
        }
        else 
        {
            lopend = false;
        }
        
        if (serieId!=0 && serie!= null && landvanoorsprong!=null && eerstepublcatie !=null && lopend!=null)
        {

            serieRepo.Update(serieId,serie,landvanoorsprong,eerstepublcatie,lopend);
            return Redirect("/serie/seriebeheer");

        }
        else
        {
            return Page();
        }
    }
    
}