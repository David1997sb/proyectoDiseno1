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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            USUARIO viewmodel = new USUARIO();

            try
            {
                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var d = db.USUARIO.Find(id);
                    viewmodel.USU_CODIGO = d.USU_CODIGO;
                    viewmodel.USU_IDENTIFICACION = d.USU_IDENTIFICACION;
                    viewmodel.USU_EMAIL = d.USU_EMAIL;
                    viewmodel.USU_ESTADO = d.USU_ESTADO;
                    viewmodel.USU_FEC_NAC = d.USU_FEC_NAC;
                    viewmodel.USU_NOMBRE = d.USU_NOMBRE;
                    viewmodel.USU_PASSWORD = d.USU_PASSWORD;
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
            List<InfoUsuario> lst;
            try
            {

                using (RESERVASEntities db = new RESERVASEntities())
                {
                    lst = (from d in db.USUARIO
                           select new InfoUsuario
                           {
                               USU_CODIGO = d.USU_CODIGO,
                               USU_EMAIL = d.USU_EMAIL,
                               USU_ESTADO = d.USU_ESTADO,
                               USU_PASSWORD = d.USU_PASSWORD,
                               USU_NOMBRE = d.USU_NOMBRE,
                               USU_FEC_NAC = d.USU_FEC_NAC,
                               USU_IDENTIFICACION = d.USU_IDENTIFICACION

                           }).OrderBy(p => p.USU_CODIGO).ToList();
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
        public IHttpActionResult Ingresar(USUARIO usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (RegistrarUsuario(usuario))
                return Ok(usuario);
            else
                return InternalServerError();

        }

        private bool RegistrarUsuario(USUARIO usuario)
        {


            try
            {
                RESERVASEntities dbi = new RESERVASEntities();
                dbi.USUARIO.Add(new USUARIO
                {
                    USU_EMAIL = usuario.USU_EMAIL,
                    USU_ESTADO = usuario.USU_ESTADO,
                    USU_FEC_NAC = usuario.USU_FEC_NAC,
                    USU_IDENTIFICACION = usuario.USU_IDENTIFICACION,
                    USU_NOMBRE = usuario.USU_NOMBRE,
                    USU_PASSWORD = usuario.USU_PASSWORD

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
        public IHttpActionResult Put(USUARIO usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (ActualizarUsuario(usuario))
            {
                return Ok(usuario);
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
                return BadRequest("Id de cuenta inválido");

            if (EliminarUsuario(id))
            {
                return Ok(id);
            }
            else
            {
                return InternalServerError();
            }
        }

        private bool EliminarUsuario(int id)
        {
            try
            {
              using(RESERVASEntities db = new RESERVASEntities()) {
                    USUARIO usuario = db.USUARIO.Where(x => x.USU_CODIGO == id).FirstOrDefault();
                    db.USUARIO.Remove(usuario);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ActualizarUsuario(USUARIO usuario)
        {
            try
            {

                using (RESERVASEntities db = new RESERVASEntities())
                {
                    var table = db.USUARIO.Find(usuario.USU_CODIGO);
                    table.USU_EMAIL = usuario.USU_EMAIL;
                    table.USU_ESTADO = usuario.USU_ESTADO;
                    table.USU_FEC_NAC = usuario.USU_FEC_NAC;
                    table.USU_IDENTIFICACION = usuario.USU_IDENTIFICACION;
                    table.USU_NOMBRE = usuario.USU_NOMBRE;
                    table.USU_PASSWORD = usuario.USU_PASSWORD;
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
