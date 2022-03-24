using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages
{
    public class DrukBeheerModel : PageModel
    {
        //for deleting creators
        [BindProperty]
        public int dcreatorid { get; set; }
        [BindProperty]
        public int ddrukid { get; set; }
        [BindProperty]
        public string drol { get; set; }

        //for adding creators
        [BindProperty]
        public int ncreatorid { get; set; }
        [BindProperty]
        public int ndrukid { get; set; }
        [BindProperty]
        public string nrol { get; set; }

        //for updating
        [BindProperty]
        public int edrukid { get; set; }
        [BindProperty]
        public int edruknummer  { get; set; }
        [BindProperty]
        public DateTime edrukdatum { get; set; }
        [BindProperty]
        public string euitvoering { get; set; }
        [BindProperty]
        public int eoplage { get; set; }
        [BindProperty]
        public double ewaarde { set; get; }
        [BindProperty]
        public int eisbn { set; get; }
        [BindProperty]
        public int estripboekid { set; get; }
        [BindProperty]
        public int euitgeverid { set; get; }

        //for inserting
        [BindProperty]
        public int idruknummer { get; set; }
        [BindProperty]
        public DateTime idrukdatum { get; set; }
        [BindProperty]
        public string iuitvoering { get; set; }
        [BindProperty]
        public int ioplage { get; set; }
        [BindProperty]
        public double iwaarde { set; get; }
        [BindProperty]
        public int iisbn { set; get; }
        [BindProperty]
        public int istripboekid { set; get; }
        [BindProperty]
        public int iuitgeverid { set; get; }

        //for feedback
        public int triggerpoint { get; set; }
        //for deleting
        [BindProperty]
        public int delete { get; set; }


        public IEnumerable<CreatorModel> creatorModels { get; set; }
        public IEnumerable<DrukModel> drukken { get; set; } = new List<DrukModel>();
        public IEnumerable<UitgeverModel> uitgevers = new List<UitgeverModel>();
        public UitgeverRepo UitgeverRepo = new UitgeverRepo();
        public WerktAanRepo WerktAanRepo = new WerktAanRepo();
        public CreatorRepo creatorRepo = new CreatorRepo(); 
        public IEnumerable<StripboekModel> stripboeken { get; set; }
        public StripBoekRepo stripBoekRepo = new StripBoekRepo();



        public IEnumerable<WerktAanModel> WerktAanModels { get; set; }


        public DrukRepo drukRepo = new DrukRepo();

        public void OnPost() 
        {
            if (delete != 0 )
            {
                drukRepo.DeleteSingle(delete);
                triggerpoint = 7;
            }

            if (ddrukid != 0 && dcreatorid != 0 && drol != "")
            {
                WerktAanRepo.DeleteSingle(ddrukid, dcreatorid, drol);
                triggerpoint = 5;
            }
            if (ncreatorid != 0 && ndrukid != 0 && nrol != "")
            {
                if (WerktAanRepo.count(ndrukid, ncreatorid, nrol) == 0)
                {
                    WerktAanRepo.insert(ndrukid, ncreatorid, nrol);
                    triggerpoint = 4;
                }
                else 
                {
                 triggerpoint = 6; 
                }
              
            }
            if (edrukid != 0 && edruknummer != null && edrukdatum != null && eisbn != 0 && estripboekid != 0 && euitgeverid != 0 && ewaarde != 0 && eoplage != 0 && euitvoering != "" )
            {
                drukRepo.update(estripboekid, edruknummer, edrukdatum, euitvoering, eoplage, ewaarde, eisbn, euitgeverid, edrukid);
                triggerpoint = 3;
            }
            if  (idruknummer != null && idrukdatum != null && iisbn != 0 && istripboekid != 0 && iuitgeverid != 0 && iwaarde != 0 && ioplage != 0 && iuitvoering != "")
            {
                if (drukRepo.count(iisbn) == 0)
                {
                    drukRepo.insert(istripboekid, idruknummer, idrukdatum, iuitvoering, ioplage, iwaarde, iisbn, iuitgeverid);
                    triggerpoint = 2;
                }
                else
                {
                    triggerpoint = 1;
                }
               
            }
            OnAll();
        }

        public void OnGet()
        {
            OnAll();
        }

        public void OnAll()
        {
            creatorModels = creatorRepo.Get();
            drukken = drukRepo.Get();
            uitgevers = UitgeverRepo.Get();
            foreach (var item in drukken)
            {
                foreach (var item2 in WerktAanRepo.Get(item.drukId))
                {
                    item.werkenAan.Add(item2);
                }
            }

            stripboeken = stripBoekRepo.Get();

        }
    }
}
