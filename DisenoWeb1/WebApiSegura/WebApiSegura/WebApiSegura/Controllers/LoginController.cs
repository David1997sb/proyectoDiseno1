using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Usuario usuarioValidado = ValidarUsuario(login);
            if (!string.IsNullOrEmpty(usuarioValidado.USU_IDENTIFICACION))
            {
                usuarioValidado.CadenaToken = TokenGenerator.GenerateTokenJwt(login.Username);

                return Ok(usuarioValidado);
            }
            else
            {
                return Unauthorized();
            }

        }

        private Usuario ValidarUsuario(LoginRequest login)
        {
            Models.Usuario usuario = new Models.Usuario();

            using (SqlConnection connection =
                 new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(" SELECT USU_CODIGO, USU_IDENTIFICACION, USU_NOMBRE, " +
                                                        "USU_PASSWORD, USU_EMAIL, USU_ESTADO, USU_FEC_NAC " +
                                                            " FROM USUARIO WHERE USU_IDENTIFICACION = @USU_IDENTIFICACION ", connection);

                command.Parameters.AddWithValue("@USU_IDENTIFICACION", login.Username);

                connection.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    if (login.Password.Equals(dr.GetString(3)))
                    {
                        usuario.USU_CODIGO = dr.GetInt32(0);
                        usuario.USU_IDENTIFICACION = dr.GetString(1);
                        usuario.USU_NOMBRE = dr.GetString(2).Trim();
                        usuario.USU_PASSWORD = dr.GetString(3);
                        usuario.USU_EMAIL = dr.GetString(4).Trim();

                        usuario.USU_ESTADO = dr.GetString(5).Trim();
                        usuario.USU_FEC_NAC = dr.GetDateTime(6);
                    }
                }
                connection.Close();
            }

            return usuario;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Usuario usuario)
        {
            if (usuario == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                using (SqlConnection connection =
               new System.Data.SqlClient.SqlConnection(
            System.Configuration.ConfigurationManager.ConnectionStrings
            ["RESERVAS"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand(" INSERT INTO USUARIO " +
                        "(  USU_IDENTIFICACION, " +
                        "    USU_NOMBRE, " +
                        "    USU_PASSWORD, " +
                        "    USU_EMAIL, " +
                        "    USU_FEC_NAC, " +
                        "    USU_ESTADO) VALUES " +
                        "( @USU_IDENTIFICACION, " +
                        "    @USU_NOMBRE, " +
                        "    @USU_PASSWORD, " +
                        "    @USU_EMAIL, " +
                        "    @USU_FEC_NAC, " +
                        "    @USU_ESTADO)", connection);

                    command.Parameters.AddWithValue("@USU_IDENTIFICACION", usuario.USU_IDENTIFICACION);
                    command.Parameters.AddWithValue("@USU_NOMBRE", usuario.USU_NOMBRE);
                    command.Parameters.AddWithValue("@USU_PASSWORD", usuario.USU_PASSWORD);
                    command.Parameters.AddWithValue("@USU_EMAIL", usuario.USU_EMAIL);
                    command.Parameters.AddWithValue("@USU_FEC_NAC", usuario.USU_FEC_NAC);
                    command.Parameters.AddWithValue("@USU_ESTADO", usuario.USU_ESTADO);


                    connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();
                    if (filasAfectadas < 0)

                        return InternalServerError();
                }
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }

            return Ok(usuario);
        }

    }
}

