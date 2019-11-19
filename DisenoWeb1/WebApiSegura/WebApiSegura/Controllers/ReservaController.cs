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
    [RoutePrefix("api/reserva")]
    public class ReservaController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Reserva reserva = new Reserva();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT RES_CODIGO, RES_FECHA, " +
                        "RES_FECHA_INGRESO, RES_FECHA_SALIDA, RES_ESTADO, USU_CODIGO, HAB_CODIGO " +
                        "FROM RESERVA WHERE RES_CODIGO = @RES_CODIGO", connection);

                    sqlCommand.Parameters.AddWithValue("@RES_CODIGO", id);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        reserva.RES_CODIGO = sqlDataReader.GetInt32(0);
                        reserva.RES_FECHA = sqlDataReader.GetDateTime(1);
                        reserva.RES_FECHA_INGRESO = sqlDataReader.GetDateTime(2);
                        reserva.RES_FECHA_SALIDA = sqlDataReader.GetDateTime(3);
                        reserva.RES_ESTADO = sqlDataReader.GetString(4);
                        reserva.USU_CODIGO = sqlDataReader.GetInt32(5);
                        reserva.HAB_CODIGO = sqlDataReader.GetInt32(6);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(reserva);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Reserva> reservas = new List<Reserva>();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT RES_CODIGO, RES_FECHA, " +
                        "RES_FECHA_INGRESO, RES_FECHA_SALIDA, RES_ESTADO, USU_CODIGO, HAB_CODIGO "+
                        "FROM RESERVA ORDER BY RES_CODIGO", connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Reserva reserva = new Reserva()
                        {
                            RES_CODIGO = sqlDataReader.GetInt32(0),
                            RES_FECHA = sqlDataReader.GetDateTime(1),
                            RES_FECHA_INGRESO = sqlDataReader.GetDateTime(2),
                            RES_FECHA_SALIDA = sqlDataReader.GetDateTime(3),
                            RES_ESTADO = sqlDataReader.GetString(4),
                            USU_CODIGO = sqlDataReader.GetInt32(5),
                            HAB_CODIGO = sqlDataReader.GetInt32(6)
                    };
                        reservas.Add(reserva);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(reservas);
        }



        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Reserva reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (RegistrarReserva(reserva))
                return Ok(reserva);
            else
                return InternalServerError();
           
        }


        private bool RegistrarReserva(Reserva reserva)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection connection =
                   new SqlConnection(
                       System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO RESERVA(RES_FECHA, " +
                        "RES_FECHA_INGRESO, RES_FECHA_SALIDA, RES_ESTADO, USU_CODIGO, HAB_CODIGO ) VALUES  " +
                        "(@RES_FECHA, @RES_FECHA_INGRESO, @RES_FECHA_SALIDA, @RES_ESTADO, @USU_CODIGO, @HAB_CODIGO) ",
                        connection);

                    sqlCommand.Parameters.AddWithValue("@RES_FECHA", reserva.RES_FECHA);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_INGRESO", reserva.RES_FECHA_INGRESO);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_SALIDA", reserva.RES_FECHA_SALIDA);
                    sqlCommand.Parameters.AddWithValue("@RES_ESTADO", reserva.RES_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@USU_CODIGO", reserva.USU_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@HAB_CODIGO", reserva.HAB_CODIGO);

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
        public IHttpActionResult Put(Reserva reserva)
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
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand(" DELETE RESERVA " +
                                       " WHERE RES_CODIGO = @RES_CODIGO ", connection);

                    command.Parameters.AddWithValue("@RES_CODIGO", id);

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

        private bool ActualizarReserva(Reserva reserva)
        {
            try
            {
                using (SqlConnection connection =
                       new System.Data.SqlClient.SqlConnection(
                           System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand command =
                        new SqlCommand( " UPDATE RESERVA " +
                                        " SET RES_FECHA = @RES_FECHA, " +
                                        " RES_FECHA_INGRESO = @RES_FECHA_INGRESO, " +
                                        " RES_FECHA_SALIDA = @RES_FECHA_SALIDA, " +
                                        " RES_ESTADO = @RES_ESTADO, " +
                                        " USU_CODIGO = @USU_CODIGO, " +
                                        " HAB_CODIGO = @HAB_CODIGO " +
                                        " WHERE RES_CODIGO = @RES_CODIGO ", connection);


                    command.Parameters.AddWithValue("@RES_CODIGO", reserva.RES_CODIGO);
                    command.Parameters.AddWithValue("@RES_FECHA", reserva.RES_FECHA);
                    command.Parameters.AddWithValue("@RES_FECHA_INGRESO", reserva.RES_FECHA_INGRESO);
                    command.Parameters.AddWithValue("@RES_FECHA_SALIDA", reserva.RES_FECHA_SALIDA);
                    command.Parameters.AddWithValue("@RES_ESTADO", reserva.RES_ESTADO);
                    command.Parameters.AddWithValue("@USU_CODIGO", reserva.USU_CODIGO);
                    command.Parameters.AddWithValue("@HAB_CODIGO", reserva.HAB_CODIGO);

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
