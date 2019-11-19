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
    [RoutePrefix("api/habitacion")]
    public class HabitacionController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            HABITACION viewmodel = new HABITACION();

            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var d = db.HABITACION.Find(id);
                    viewmodel.HAB_CODIGO = d.HAB_CODIGO;
                    viewmodel.HAB_CANT_HUESP = d.HAB_CANT_HUESP;
                    viewmodel.HAB_ESTADO = d.HAB_ESTADO;
                    viewmodel.HAB_PRECIO = d.HAB_PRECIO;
                    viewmodel.HAB_TIPO = d.HAB_TIPO;
                    viewmodel.HOT_CODIGO = d.HOT_CODIGO;

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
            List<InfoHabitacion> lst;
            try
            {

                using (RESERVASEntities db = new RESERVASEntities())
                {
                    lst = (from d in db.HABITACION
                           select new InfoHabitacion
                           {
                               HAB_TIPO = d.HAB_TIPO,
                               HAB_PRECIO = d.HAB_PRECIO,
                               HAB_ESTADO = d.HAB_ESTADO,
                               HAB_CANT_HUESP = d.HAB_CANT_HUESP,
                               HAB_CODIGO = d.HAB_CODIGO,
                               HOT_CODIGO = d.HOT_CODIGO

                           }).OrderBy(p => p.HAB_CODIGO).ToList();
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
        public IHttpActionResult Ingresar(HABITACION habitacion)
        {
            if (habitacion == null)
                return BadRequest();

            if (RegistrarHabitacion(habitacion))
                return Ok(habitacion);
            else
                return InternalServerError();

        }

        private bool RegistrarHabitacion(HABITACION habitacion)
        {

            try
            {
                RESERVASEntities dbi = new RESERVASEntities();
                dbi.HABITACION.Add(new HABITACION
                {
                    HAB_CANT_HUESP = habitacion.HAB_CANT_HUESP,
                    HAB_ESTADO = habitacion.HAB_ESTADO,
                    HAB_PRECIO = habitacion.HAB_PRECIO,
                    HAB_TIPO = habitacion.HAB_TIPO,
                    HOT_CODIGO = habitacion.HOT_CODIGO

                });
                dbi.SaveChanges();
                return (true);
            }

            catch (Exception ex)
            {
                throw ex;
            }
           
        }


        [HttpPut]
        public IHttpActionResult Put(HABITACION habitacion)
        {
            if (habitacion == null)
                return BadRequest();

            if (ActualizarHabitacion(habitacion))
            {
                return Ok(habitacion);
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
                return BadRequest("Id de habitación inválido");

            if (EliminarHabitacion(id))
            {
                return Ok(id);
            }
            else
            {
                return InternalServerError();
            }
        }

        private bool EliminarHabitacion(int id)
        {
            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {

                    HABITACION habitacion = db.HABITACION.Where(x => x.HAB_CODIGO == id).FirstOrDefault();
                    db.HABITACION.Remove(habitacion);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ActualizarHabitacion(HABITACION habitacion)
        {
            try
            {
                using (RESERVASEntities db = new RESERVASEntities()) {
                    var table = db.HABITACION.Find(habitacion.HAB_CODIGO);
                    table.HAB_CANT_HUESP = habitacion.HAB_CANT_HUESP;
                    table.HAB_ESTADO = habitacion.HAB_ESTADO;
                    table.HAB_PRECIO = habitacion.HAB_PRECIO;
                    table.HAB_TIPO = habitacion.HAB_TIPO;

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
