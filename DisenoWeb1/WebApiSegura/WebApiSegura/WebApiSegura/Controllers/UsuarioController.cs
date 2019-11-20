using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
using System.Data.SqlClient;
namespace WebApiSegura.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT USU_CODIGO, USU_IDENTIFICACION, " +
                        "USU_NOMBRE, USU_PASSWORD, USU_EMAIL, USU_ESTADO, USU_FEC_NAC " +
                        "FROM USUARIO WHERE USU_CODIGO = @USU_CODIGO", connection);

                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", id);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        usuario.USU_CODIGO = sqlDataReader.GetInt32(0);
                        usuario.USU_IDENTIFICACION = sqlDataReader.GetString(1);
                        usuario.USU_NOMBRE = sqlDataReader.GetString(2);
                        usuario.USU_PASSWORD = sqlDataReader.GetString(3);
                        usuario.USU_EMAIL = sqlDataReader.GetString(4);
                        usuario.USU_ESTADO = sqlDataReader.GetString(5);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(usuario);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT USU_CODIGO, USU_IDENTIFICACION, " +
                        "USU_NOMBRE, USU_PASSWORD, USU_EMAIL, USU_ESTADO " +
                        "FROM USUARIO ORDER BY USU_CODIGO", connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Usuario usuario = new Usuario()
                        {
                            USU_CODIGO = sqlDataReader.GetInt32(0),
                            USU_IDENTIFICACION = sqlDataReader.GetString(1),
                            USU_NOMBRE = sqlDataReader.GetString(2),
                            USU_PASSWORD = sqlDataReader.GetString(3),
                            USU_EMAIL = sqlDataReader.GetString(4),
                            USU_ESTADO = sqlDataReader.GetString(5)
                        };
                        usuarios.Add(usuario);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(usuarios);
        }



        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (RegistrarUsuario(usuario))
                return Ok(usuario);
            else
                return InternalServerError();
           
        }


        private bool RegistrarUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection connection =
                   new SqlConnection(
                       System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO USUARIO(USU_IDENTIFICACION, " +
                        "USU_NOMBRE, USU_PASSWORD, USU_EMAIL, USU_ESTADO) VALUES  " +
                        "(@USU_IDENTIFICACION, @USU_NOMBRE, @USU_PASSWORD, @USU_EMAIL, @USU_ESTADO) ",
                        connection);

                    sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICACION", usuario.USU_IDENTIFICACION);
                    sqlCommand.Parameters.AddWithValue("@USU_NOMBRE", usuario.USU_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@USU_PASSWORD", usuario.USU_PASSWORD);
                    sqlCommand.Parameters.AddWithValue("@USU_EMAIL", usuario.USU_EMAIL);
                    sqlCommand.Parameters.AddWithValue("@USU_ESTADO", usuario.USU_ESTADO);

                    connection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;
        }


        [HttpPut]
        public IHttpActionResult Put(Usuario usuario)
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
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand(" DELETE USUARIO " +
                                       " WHERE USU_CODIGO = @USU_CODIGO ", connection);

                    command.Parameters.AddWithValue("@USU_CODIGO", id);

                    connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand( " UPDATE USUARIO " +
                                        " SET USU_IDENTIFICACION = @USU_IDENTIFICACION, " +
                                        " USU_NOMBRE = @USU_NOMBRE, " +
				        " USU_PASSWORD = @USU_PASSWORD, " +
					" USU_EMAIL = @USU_EMAIL, " +
					" USU_ESTADO = @USU_ESTADO " +
                                        " WHERE USU_CODIGO = @USU_CODIGO ", connection);


                   	command.Parameters.AddWithValue("@USU_IDENTIFICACION", usuario.USU_IDENTIFICACION);
                    	command.Parameters.AddWithValue("@USU_NOMBRE", usuario.USU_NOMBRE);
                    	command.Parameters.AddWithValue("@USU_PASSWORD", usuario.USU_PASSWORD);
			command.Parameters.AddWithValue("@USU_EMAIL", usuario.USU_EMAIL);
			command.Parameters.AddWithValue("@USU_ESTADO", usuario.USU_ESTADO);
			command.Parameters.AddWithValue("@USU_CODIGO", usuario.USU_CODIGO);

                    connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    connection.Close();
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
