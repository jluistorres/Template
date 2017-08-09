using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.Controllers
{
    public class ArticuloController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BagList()
        {
            System.Threading.Thread.Sleep(1000);
            var model = (List<ArticuloItemBag>)Session["ArticuloBag"];
            return PartialView("Partial/Articulo", model);
        }

        [HttpPost]
        public void BagRemove(int id)
        {
            var bolsa = (List<ArticuloItemBag>)Session["ArticuloBag"];
            if (bolsa != null)
            {
                var item = bolsa.FirstOrDefault(x => x.IdArticulo == id);
                if (item != null)
                {
                    bolsa.Remove(item);
                }
            }
        }

        [HttpPost]
        public void BagRemoveAll()
        {
            Session["ArticuloBag"] = null;
        }
	}
}