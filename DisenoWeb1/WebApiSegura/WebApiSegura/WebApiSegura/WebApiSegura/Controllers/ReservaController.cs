using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
using System.Data.SqlClient;
using WebApiSeguro.Models.ViewModel;

namespace WebApiSegura.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/reserva")]
    public class ReservaController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            RESERVA viewmodel = new RESERVA();

            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var d = db.RESERVA.Find(id);
                    viewmodel.RES_CODIGO = d.RES_CODIGO;
                    viewmodel.HABITACION = d.HABITACION;
                    viewmodel.HAB_CODIGO = d.HAB_CODIGO;
                    viewmodel.RES_ESTADO = d.RES_ESTADO;
                    viewmodel.RES_FECHA = d.RES_FECHA;
                    viewmodel.RES_FECHA_INGRESO = d.RES_FECHA_INGRESO;
                    viewmodel.RES_FECHA_SALIDA = d.RES_FECHA_SALIDA;
                    viewmodel.USU_CODIGO = d.USU_CODIGO;

                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<InfoReserva> lst;
            try
            {

                using (RESERVASEntities db = new RESERVASEntities())
                {
                    lst = (from d in db.RESERVA
                           select new InfoReserva
                           {
                               RES_CODIGO = d.RES_CODIGO,
                               RES_ESTADO = d.RES_ESTADO,
                               RES_FECHA = d.RES_FECHA,
                               RES_FECHA_INGRESO = d.RES_FECHA_INGRESO,
                               RES_FECHA_SALIDA = d.RES_FECHA_SALIDA,
                               RES_TOTAL = d.RES_TOTAL,
                               USU_CODIGO = d.USU_CODIGO,
                               HAB_CODIGO = d.HAB_CODIGO

                           }).OrderBy(p => p.RES_CODIGO).ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return Ok(lst);
        }

        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(RESERVA reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (RegistrarReserva(reserva))
                return Ok(reserva);
            else
                return InternalServerError();

        }

        private bool RegistrarReserva(RESERVA reserva)
        {

            try
            {
                RESERVASEntities dbi = new RESERVASEntities();
                dbi.RESERVA.Add(new RESERVA
                {
                    RES_ESTADO = reserva.RES_ESTADO,
                    RES_FECHA = reserva.RES_FECHA,
                    RES_FECHA_INGRESO = reserva.RES_FECHA_INGRESO,
                    RES_FECHA_SALIDA = reserva.RES_FECHA_SALIDA,
                    RES_TOTAL = reserva.RES_TOTAL,
                    USU_CODIGO = reserva.USU_CODIGO,
                    HAB_CODIGO = reserva.HAB_CODIGO

                });
                dbi.SaveChanges();
                return (true);
            }

            catch (Exception ex)
            {
                throw  ex;
            }
          
        }


        [HttpPut]
        public IHttpActionResult Put(RESERVA reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (ActualizarReserva(reserva))
            {
                return Ok(reserva);
            }
            else
            {
                return InternalServerError();
            }
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id de reserva inválido");

            if (EliminarReserva(id))
            {
                return Ok(id);
            }
            else
            {
                return InternalServerError();
            }
        }

        private bool EliminarReserva(int id)
        {
            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {

                    RESERVA reserva = db.RESERVA.Where(x => x.RES_CODIGO == id).FirstOrDefault();
                    db.RESERVA.Remove(reserva);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ActualizarReserva(RESERVA reserva)
        {
            try
            {
                using (RESERVASEntities db = new RESERVASEntities()) {
                    var table = db.RESERVA.Find(reserva.RES_CODIGO);
                    table.RES_ESTADO = reserva.RES_ESTADO;
                    table.RES_FECHA = reserva.RES_FECHA;
                    table.RES_FECHA_INGRESO = reserva.RES_FECHA_INGRESO;
                    table.RES_FECHA_SALIDA = reserva.RES_FECHA_SALIDA;
                    table.RES_TOTAL = reserva.RES_TOTAL;
                    db.Entry(table).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
