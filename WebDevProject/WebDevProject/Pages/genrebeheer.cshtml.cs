using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class genrebeheer : PageModel
{
    public IEnumerable<GenreModel> genres { get; set; }

    private GenreRepo genreRepo = new GenreRepo();
    [BindProperty(SupportsGet = true)] [Required]  public string genre { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cookie") == null)
        {
            return new RedirectToPageResult("/Index");

        }
        else
        {
            genres = genreRepo.Get();
        }
        return Page();
    }

    public void OnPostDelete([FromForm] string? genre)
    {
        genreRepo.DeleteSingle(genre);
        genres = genreRepo.Get();
        TempData["AlertMessage"] = "Genre Deleted successfully...";


    }
    public void OnPostAdd()
    {
        if (genre!= null)
        {
            genreRepo.insert(genre);
            genres = genreRepo.Get();
            TempData["AlertMessage"] = "Genre Added successfully...";

        }
    }
    public void OnPostUpdate([FromForm] string? genre, [FromForm] int hiddengenre)
    {
        if (hiddengenre !=0 && genre != null) 
        {
            genreRepo.Update(hiddengenre,genre);
            genres = genreRepo.Get();
            TempData["AlertMessage"] = "Genre Updated successfully...";


        }
    }
}