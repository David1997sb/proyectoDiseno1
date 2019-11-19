using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservas.Models;
using AppReservas.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AppReservas
{
    public partial class frmHotel : System.Web.UI.Page
    {
        HotelManager hotelManager = new HotelManager();
        IEnumerable<Models.Hotel> hoteles = new ObservableCollection<Models.Hotel>();

        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(
                HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);


            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!VG.usuarioActual.EstadoSesion.Equals("A") ||
                (DateTime.Now > VG.usuarioActual.Token.ValidTo))
            {
                Response.Redirect("~Login.aspx");
            }
            else
                InicializarControles();
        }

        private async void InicializarControles()
        {
            hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);
            grdHoteles.DataSource = hoteles.ToList();
            grdHoteles.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
             Models.Hotel hotel = new Models.Hotel(){
                HOT_NOMBRE = txtNombre.Text,
                HOT_DIRECCION = txtDireccion.Text,
                HOT_EMAIL = txtEmail.Text
            };

            Models.Hotel hotelIngresado = 
                await hotelManager.Ingresar(hotel, VG.usuarioActual.CadenaToken); 

            if(!string.IsNullOrEmpty(hotelIngresado.HOT_NOMBRE))
            {
                lblResultado.Text = "Hotel ingresado exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al ingresar el hotel";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }

        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            Models.Hotel hotel = new Models.Hotel()
            {
                HOT_CODIGO = Convert.ToInt32(txtCodigo.Text),
                HOT_NOMBRE = txtNombre.Text,
                HOT_DIRECCION = txtDireccion.Text,
                HOT_EMAIL = txtEmail.Text
            };

            Models.Hotel hotelModificado =
                await hotelManager.Actualizar(hotel, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(hotelModificado.HOT_NOMBRE))
            {
                lblResultado.Text = "Hotel modificado exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al modificar el hotel";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {

            string codigoHotelEliminado =
                await hotelManager.Eliminar(txtCodigo.Text, 
                VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoHotelEliminado))
            {
                lblResultado.Text = "Hotel eliminado exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al eliminar el hotel";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }
    }
}