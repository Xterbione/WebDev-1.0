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
    public void OnGet()
    {
        Series = serieRepo.Get();
    }
    
}