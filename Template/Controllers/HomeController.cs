using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.Controllers
{
    public class HomeController : BaseController
    {        
        public ActionResult Index()
        {
            List<ArticuloItemBag> articulos = new List<ArticuloItemBag>();

            for (int i = 0; i < 5; i++)
            {
                articulos.Add(new ArticuloItemBag()
                {
                    IdArticulo = i + 1,
                    Nombre = "Articulo " + (i + 1),
                    Detalle = "",
                    Cantidad = i + 1,
                    Precio = 2 * (i + 1),
                    UM = "UND",
                    UM_Nombre = "UNIDAD",
                    IdMoneda = "01SOL",
                    Moneda = "S/"
                });
            }

            Session["ArticuloBag"] = articulos;

            //Notificaciones de prueba
            List<NotificacionItemBag> notificaciones = new List<NotificacionItemBag>();

            for (int i = 0; i < 5; i++)
            {
                notificaciones.Add(new NotificacionItemBag()
                {
                    IdNotificacion = i + 1,
                    IdEmisor = 11,
                    Emisor = "Emisor 1",
                    Titulo = "Notificación de prueba",
                    Detalle = "Mensaje de consola",
                    Tipo = 1,
                    Url = null,
                    Fecha = DateTime.Now,
                    IdReceptor = 1
                });
            }

            Session["NotificacionBag"] = notificaciones;
            
            return View();
        }
	}
}