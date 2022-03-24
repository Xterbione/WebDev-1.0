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

    public void OnGet()
    {
        genres = genreRepo.Get();
    }

    public void OnPostDelete([FromForm] string? hiddenGenre)
    {
        genreRepo.DeleteSingle(hiddenGenre);
        genres = genreRepo.Get();

    }
    public void OnPostAdd()
    {
        if (genre!= null)
        {
            genreRepo.insert(genre);
            genres = genreRepo.Get();

        }
    }
    public void OnPostUpdate([FromForm] string? genre, [FromForm] int hiddengenre)
    {
        if (hiddengenre !=0 && genre != null) 
        {
            genreRepo.Update(hiddengenre,genre);
            genres = genreRepo.Get();

        }
    }
}