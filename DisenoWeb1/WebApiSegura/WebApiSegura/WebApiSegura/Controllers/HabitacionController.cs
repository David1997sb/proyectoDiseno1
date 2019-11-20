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
    [RoutePrefix("api/habitacion")]
    public class HabitacionController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Habitacion habitacion = new Habitacion();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT HAB_CODIGO, HAB_CANT_HUESP, " +
                        "HAB_TIPO, HAB_PRECIO, HAB_ESTADO, HOT_CODIGO" +
                        "FROM HABITACION WHERE HAB_CODIGO = @HAB_CODIGO", connection);

                    sqlCommand.Parameters.AddWithValue("@HAB_CODIGO", id);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        habitacion.HAB_CODIGO = sqlDataReader.GetInt32(0);
                        habitacion.HAB_CANT_HUESP = sqlDataReader.GetInt32(1);
                        habitacion.HAB_TIPO = sqlDataReader.GetString(2);
                        habitacion.HAB_PRECIO = sqlDataReader.GetInt32(3);
                        habitacion.HAB_ESTADO = sqlDataReader.GetString(4);
                        habitacion.HOT_CODIGO = sqlDataReader.GetInt32(5);
   
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(habitacion);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Habitacion> habitaciones = new List<Habitacion>();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT HAB_CODIGO, HAB_CANT_HUESP, " +
                        "HAB_TIPO, HAB_PRECIO, HAB_ESTADO, HOT_CODIGO" +
                        "FROM HABITACION ORDER BY HAB_CODIGO", connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Habitacion habitacion = new Habitacion()
                        {
                            HAB_CODIGO = sqlDataReader.GetInt32(0),
                            HAB_CANT_HUESP = sqlDataReader.GetInt32(1),
                            HAB_TIPO = sqlDataReader.GetString(2),
                            HAB_PRECIO = sqlDataReader.GetInt32(3),
                            HAB_ESTADO = sqlDataReader.GetString(4),
                            HOT_CODIGO = sqlDataReader.GetInt32(5)
                        };
                        habitaciones.Add(habitacion);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(habitaciones);
        }



        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Habitacion habitacion)
        {
            if (habitacion == null)
                return BadRequest();

            if (RegistrarHabitacion(habitacion))
                return Ok(habitacion);
            else
                return InternalServerError();
           
        }


        private bool RegistrarHabitacion(Habitacion habitacion)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection connection =
                   new SqlConnection(
                       System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO HABITACION(HAB_CANT_HUESP, " +
                        "HAB_TIPO, HAB_PRECIO, HAB_ESTADO, HOT_CODIGO) VALUES  " +
                        "( @HAB_CANT_HUESP, @HAB_TIPO, @HAB_PRECIO, @HAB_ESTADO, @HOT_CODIGO) ",
                        connection);

                    sqlCommand.Parameters.AddWithValue("@HAB_CANT_HUESP", habitacion.HAB_CANT_HUESP);
                    sqlCommand.Parameters.AddWithValue("@HAB_TIPO", habitacion.HAB_TIPO);
                    sqlCommand.Parameters.AddWithValue("@HAB_PRECIO", habitacion.HAB_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@HAB_ESTADO", habitacion.HAB_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", habitacion.HAB_CODIGO);

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
        public IHttpActionResult Put(Habitacion habitacion)
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
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand(" DELETE HABITACION " +
                                       " WHERE HAB_CODIGO = @HAB_CODIGO ", connection);

                    command.Parameters.AddWithValue("@HAB_CODIGO", id);

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

        private bool ActualizarHabitacion(Habitacion habitacion)
        {
            try
            {
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand( " UPDATE HABITACION " +
                                        " SET HAB_CANT_HUESP = @HAB_CANT_HUESP, " +
                                        " HAB_TIPO = @HAB_TIPO, " +
                                        " HAB_PRECIO = @HAB_PRECIO, " +
                                        " HAB_ESTADO = @HAB_ESTADO, " +
                                        " HOT_CODIGO = @HOT_CODIGO, " +
                                        " WHERE HAB_CODIGO = @HAB_CODIGO ", connection);

                    command.Parameters.AddWithValue("@HAB_CODIGO", habitacion.HAB_CODIGO); 
                    command.Parameters.AddWithValue("@HAB_CANT_HUESP", habitacion.HAB_CANT_HUESP);
                    command.Parameters.AddWithValue("@HAB_TIPO", habitacion.HAB_TIPO);
			        command.Parameters.AddWithValue("@HAB_PRECIO", habitacion.HAB_PRECIO);
			        command.Parameters.AddWithValue("@HAB_ESTADO", habitacion.HAB_ESTADO);
			        command.Parameters.AddWithValue("@HOT_CODIGO", habitacion.HOT_CODIGO);
			        

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
