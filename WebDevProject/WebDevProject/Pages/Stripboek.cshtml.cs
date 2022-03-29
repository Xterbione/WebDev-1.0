using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Stripboek : PageModel
{
    private DrukRepo DrukRepo = new ();
    private GenreRepo GenreRepo = new ();
    private StripboekRepo stripboekRepo = new ();
    private SerieRepo SerieRepo = new ();
    
    public IEnumerable<DrukModel> Drukken { get; set; }
    public StripboekModel StripboekModel { get; set; }
    

    
    public void OnGet([FromRoute]int stripboekId)
    {
        StripboekModel = stripboekRepo.Get(stripboekId);
        StripboekModel.GenreModel = GenreRepo.Get(StripboekModel.genre_id);
        //StripboekModel.SerieModel = SerieRepo.Get(StripboekModel.serie_id);
        Drukken = DrukRepo.Get(stripboekId);
    }
}