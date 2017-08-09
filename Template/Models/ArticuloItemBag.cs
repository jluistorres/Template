using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template
{
    public class ArticuloItemBag
    {
        public int IdArticulo { get; set; }
        //public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }
        public string UM { get; set; }
        public string UM_Nombre { get; set; }
        public string IdMoneda { get; set; }
        public string Moneda { get; set; }
    }
}