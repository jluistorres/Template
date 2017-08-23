using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.Controllers
{
    public class NotificacionesController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BagList()
        {
            System.Threading.Thread.Sleep(1000);
            //obrNotificaciones = new brNotificaciones();

            //var model = obrNotificaciones.Listar(SessionHelper.User.IdPersonal);            

            var model = (List<NotificacionItemBag>)Session["NotificacionBag"];
            return PartialView("Partial/Notificacion", model);
        }

        [HttpPost]
        public void BagRemove(int id)
        {
            //obrNotificaciones = new brNotificaciones();
            //obrNotificaciones.Eliminar(id);

            var bolsa = (List<NotificacionItemBag>)Session["NotificacionBag"];
            if (bolsa != null)
            {
                var item = bolsa.FirstOrDefault(x => x.IdNotificacion == id);
                if (item != null)
                {
                    bolsa.Remove(item);
                }
            }
        }

        [HttpPost]
        public void BagRemoveAll()
        {
            //obrNotificaciones = new brNotificaciones();
            //obrNotificaciones.EliminarByReceptor(SessionHelper.User.IdPersonal);
            
            Session.Remove("NotificacionBag");
        }
	}
}