using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template
{
    //Esta clase debería estar en una capa de datos Entidades 
    //para usarla al momento de extraer los datos de la BD
    public class NotificacionItemBag
    {
        public long IdNotificacion { get; set; }
        public Nullable<int> IdEmisor { get; set; }
        public string Emisor { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public Nullable<int> Tipo { get; set; }
        public string Url { get; set; }
        public DateTime Fecha { get; set; }
        public Nullable<int> IdReceptor { get; set; }
    }
}