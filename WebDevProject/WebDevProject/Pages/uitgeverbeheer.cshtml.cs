using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class uitgeverbeheer : PageModel
{
    public IEnumerable<UitgeverModel> uitgevers { get; set; }
    private UitgeverRepo uitgeverRepo = new UitgeverRepo();
    [BindProperty(SupportsGet = true)] public string uitgever { get; set; }  
    public void OnGet()
    {
        uitgevers = uitgeverRepo.Get();
    }
    public void OnPostDelete([FromForm] string? uitgever)
    {
        uitgeverRepo.DeleteSingle(uitgever);
        uitgevers = uitgeverRepo.Get();

    }
    public void OnPostAdd()
    {
        if (uitgever!= null)
        {
            uitgeverRepo.insert(uitgever);
            uitgevers = uitgeverRepo.Get();
        }
    }

    public void OnPostUpdate([FromForm] string? uitgever, [FromForm] int hiddenuitgever)
    {
        if (hiddenuitgever != 0 && uitgever != null)
        {
            uitgeverRepo.Update(hiddenuitgever, uitgever);
            uitgevers = uitgeverRepo.Get();

        }
    }
}