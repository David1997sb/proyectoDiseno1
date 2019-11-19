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
    [RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            HOTEL viewmodel = new HOTEL();

            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var d = db.HOTEL.Find(id);
                    viewmodel.HOT_CODIGO = d.HOT_CODIGO;
                    viewmodel.HOT_NOMBRE = d.HOT_NOMBRE;
                    viewmodel.HOT_EMAIL = d.HOT_EMAIL;
                    viewmodel.HOT_DIRECCION = d.HOT_DIRECCION;

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
            List<InfoHotel> lst;
            try
            {

                using (RESERVASEntities db = new RESERVASEntities())
                {
                    lst = (from d in db.HOTEL
                           select new InfoHotel
                           {
                               HOT_CODIGO = d.HOT_CODIGO,
                               HOT_DIRECCION = d.HOT_DIRECCION,
                               HOT_EMAIL = d.HOT_EMAIL,
                               HOT_NOMBRE = d.HOT_NOMBRE

                           }).OrderBy(p => p.HOT_CODIGO).ToList();
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
        public IHttpActionResult Ingresar(HOTEL hotel)
        {
            if (hotel == null)
                return BadRequest();

            if (RegistrarHotel(hotel))
                return Ok(hotel);
            else
                return InternalServerError();

        }

        private bool RegistrarHotel(HOTEL hotel)
        {

            try
            {
                RESERVASEntities dbi = new RESERVASEntities();
                dbi.HOTEL.Add(new HOTEL
                {
                    HOT_DIRECCION = hotel.HOT_DIRECCION,
                    HOT_EMAIL = hotel.HOT_EMAIL,
                    HOT_NOMBRE = hotel.HOT_NOMBRE

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
        public IHttpActionResult Put(HOTEL hotel)
        {
            if (hotel == null)
                return BadRequest();

            if (ActualizarHotel(hotel))
            {
                return Ok(hotel);
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
                return BadRequest("Id de hotel inválido");

            if (EliminarHotel(id))
            {
                return Ok(id);
            }
            else
            {
                return InternalServerError();
            }
        }

        private bool EliminarHotel(int id)
        {
            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {

                    HOTEL evento = db.HOTEL.Where(x => x.HOT_CODIGO == id).FirstOrDefault();
                    db.HOTEL.Remove(evento);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ActualizarHotel(HOTEL hotel)
        {
            try
            {
              
                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var tabla = db.HOTEL.Find(hotel.HOT_CODIGO);
                    tabla.HOT_DIRECCION = hotel.HOT_DIRECCION;
                    tabla.HOT_EMAIL = hotel.HOT_EMAIL;
                    tabla.HOT_NOMBRE = hotel.HOT_NOMBRE;

                    db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
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
