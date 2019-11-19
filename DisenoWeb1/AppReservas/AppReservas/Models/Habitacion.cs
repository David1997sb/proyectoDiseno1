using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservas.Models
{
    public class Habitacion
    {
        public int HAB_CODIGO { get; set; }
        public int HAB_CANT_HUESP { get; set; }
        public string HAB_TIPO { get; set; }
        public int HAB_PRECIO { get; set; }
        public string HAB_ESTADO { get; set; }
        public int HOT_CODIGO { get; set; }
    }
}