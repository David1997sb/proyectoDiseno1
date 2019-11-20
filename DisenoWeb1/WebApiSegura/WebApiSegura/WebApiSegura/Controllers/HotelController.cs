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
    [RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Hotel hotel = new Hotel();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT HOT_CODIGO, HOT_NOMBRE, " +
                        "HOT_EMAIL, HOT_DIRECCION" +
                        "FROM HOTEL WHERE HOT_CODIGO = @HOT_CODIGO", connection);

                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", id);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        hotel.HOT_CODIGO = sqlDataReader.GetInt32(0);
                        hotel.HOT_NOMBRE = sqlDataReader.GetString(1);
                        hotel.HOT_EMAIL = sqlDataReader.GetString(2);
                        hotel.HOT_DIRECCION = sqlDataReader.GetString(3);

                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(hotel);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Hotel> hoteles = new List<Hotel>();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT HOT_CODIGO, HOT_NOMBRE, " +
                        "HOT_EMAIL, HOT_DIRECCION " +
                        "FROM HOTEL ORDER BY HOT_CODIGO", connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Hotel hotel = new Hotel()
                        {
                            HOT_CODIGO = sqlDataReader.GetInt32(0),
                            HOT_NOMBRE = sqlDataReader.GetString(1),
                            HOT_EMAIL = sqlDataReader.GetString(2),
                            HOT_DIRECCION = sqlDataReader.GetString(3),
              
                        };
                        hoteles.Add(hotel);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(hoteles);
        }



        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Hotel hotel)
        {
            if (hotel == null)
                return BadRequest();

            if (RegistrarHotel(hotel))
                return Ok(hotel);
            else
                return InternalServerError();
           
        }


        private bool RegistrarHotel(Hotel hotel)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection connection =
                   new SqlConnection(
                       System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO HOTEL( " +
                        "HOT_NOMBRE, HOT_EMAIL, HOT_DIRECCION) VALUES  " +
                        "(@HOT_NOMBRE, @HOT_EMAIL, @HOT_DIRECCION) ",
                        connection);

                    sqlCommand.Parameters.AddWithValue("@HOT_NOMBRE", hotel.HOT_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@HOT_EMAIL", hotel.HOT_EMAIL);
                    sqlCommand.Parameters.AddWithValue("@HOT_DIRECCION", hotel.HOT_DIRECCION);


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
        public IHttpActionResult Put(Hotel hotel)
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
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand(" DELETE HOTEL " +
                                       " WHERE HOT_CODIGO = @HOT_CODIGO ", connection);

                    command.Parameters.AddWithValue("@HOT_CODIGO", id);

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

        private bool ActualizarHotel(Hotel hotel)
        {
            try
            {
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand( " UPDATE HOTEL " +
                                        " SET HOT_NOMBRE = @HOT_NOMBRE, " +
                                        " HOT_EMAIL = @HOT_EMAIL, " +
                                        " HOT_DIRECCION = @HOT_DIRECCION, " +
                                        " WHERE HOT_CODIGO = @HOT_CODIGO ", connection);

                    command.Parameters.AddWithValue("@HOT_CODIGO", hotel.HOT_CODIGO);
                    command.Parameters.AddWithValue("@HOT_NOMBRE", hotel.HOT_NOMBRE);
                    command.Parameters.AddWithValue("@HOT_EMAIL", hotel.HOT_EMAIL);
                    command.Parameters.AddWithValue("@HOT_DIRECCION", hotel.HOT_DIRECCION);

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
