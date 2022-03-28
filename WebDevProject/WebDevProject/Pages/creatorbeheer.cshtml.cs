using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class creatorbeheer : PageModel
{
    public IEnumerable<CreatorModel> creators { get; set; }
    private CreatorRepo creatorRepo = new CreatorRepo();
    [BindProperty(SupportsGet = true)] public string creator { get; set; }  
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") == null)
        {
            return new RedirectToPageResult("/Index");

        }
        else
        {
            creators = creatorRepo.Get();
        }
        return Page();
    }
    public void OnPostDelete([FromForm] string? hiddenCreator)
    {
        creatorRepo.DeleteSingle(hiddenCreator);
        creators = creatorRepo.Get();
    }
    public void OnPostAdd()
    {
        if (creator!= null)
        {
            creatorRepo.insert(creator);
            creators = creatorRepo.Get();
        }
    }
}