using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages
{
    public class stripboekbeheerModel : PageModel
    {
        //binding properties to a get form
        //only will be filled if part of the submitted form
        [BindProperty(SupportsGet = true)]
        public int ucat { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ustitel { get; set; }
        [BindProperty(SupportsGet = true)]
        public int uvolgnummer { get; set; }
        [BindProperty(SupportsGet = true)]
        public int uaantalpaginas { get; set; }
        [BindProperty(SupportsGet = true)]
        public int userie { get; set; }
        [BindProperty(SupportsGet = true)]
        public int updateid { get; set; }
       


         [BindProperty(SupportsGet = true)]
        public int ncat { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nstitel { get; set; }
        [BindProperty(SupportsGet = true)]
        public int nvolgnummer { get; set; }
        [BindProperty(SupportsGet = true)]
        public int naantalpaginas { get; set; }
        [BindProperty(SupportsGet = true)]
        public int nserie { get; set; }


        [BindProperty(SupportsGet = true)]
        public int delete { get; set; }



        public IEnumerable<StripboekModel> strips { get; set; }
        public IEnumerable<GenreModel> genres { get; set; }
        public IEnumerable<SerieModel> series { get; set; }
        StripBoekRepo sprepo = new StripBoekRepo();
        GenreRepo grepo = new GenreRepo();
        SerieRepo serieRepo = new SerieRepo();
        public void OnGet()
        {


            if (ucat != 0 && ustitel != null && uvolgnummer != null && uaantalpaginas != 0 && userie != 0 && updateid != 0)
            {
                sprepo.update(ucat, ustitel, uaantalpaginas, uvolgnummer, userie, updateid);
            }

            if (ncat != 0 && nstitel != null && nvolgnummer != null && naantalpaginas != 0 && nserie != 0)
            {
                sprepo.insert(ncat, nstitel, naantalpaginas, nvolgnummer, nserie);
            }


            if (delete != 0 )
            {
                sprepo.DeleteSingle(delete);
            }

            strips = sprepo.Get();
            genres = grepo.Get();
            series = serieRepo.Get();
        }
    }
}
