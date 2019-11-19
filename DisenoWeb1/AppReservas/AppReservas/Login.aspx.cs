using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservas.Controllers;
using AppReservas.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Security;
namespace AppReservas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario = await usuarioManager.Validar(Identificacion.Text, Password.Text);
            JwtSecurityToken securityToken;
            if(!string.IsNullOrEmpty(usuario.CadenaToken))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                securityToken = 
                    jwtHandler.ReadJwtToken(usuario.CadenaToken);
                FormsAuthentication.RedirectFromLoginPage(
                    Identificacion.Text, true);

                VG.usuarioActual = usuario;
                VG.usuarioActual.EstadoSesion = "A";
                VG.usuarioActual.Token = securityToken;
            }
            else
            {
                FailureText.Text = "Credenciales inválidas";
                FailureText.Visible = true;
            }
        }
    }
}