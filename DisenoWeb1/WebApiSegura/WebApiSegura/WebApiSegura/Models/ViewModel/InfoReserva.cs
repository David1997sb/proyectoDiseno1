using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiSegura.Models;

namespace WebApiSeguro.Models.ViewModel
{
    public class InfoReserva
    {

        public int RES_CODIGO { get; set; }
        public System.DateTime RES_FECHA { get; set; }
        public System.DateTime RES_FECHA_INGRESO { get; set; }
        public System.DateTime RES_FECHA_SALIDA { get; set; }
        public string RES_ESTADO { get; set; }
        public int USU_CODIGO { get; set; }
        public int HAB_CODIGO { get; set; }
        public double RES_TOTAL { get; set; }

        public virtual HABITACION HABITACION { get; set; }
        public virtual USUARIO USUARIO { get; set; }

    }
}