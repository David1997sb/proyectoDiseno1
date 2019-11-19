using AppReservas.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservas
{
    public partial class frmHabitacion : System.Web.UI.Page
    {
            HabitacionManager habitacionManager = new HabitacionManager();
            IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();

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
            if (!VG.usuarioActual.EstadoSesion.Equals("A") ||
                (DateTime.Now > VG.usuarioActual.Token.ValidTo))
            {
                Response.Redirect("~Login.aspx");
            }
            else
                InicializarControles();
        }

        private async void InicializarControles()
        {
            habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);
            grdHoteles.DataSource = habitaciones.ToList();
            grdHoteles.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Models.Habitacion habitacion = new Models.Habitacion()
            {
                HAB_CANT_HUESP = Convert.ToInt32(txtCantHuesp.Text),
                HAB_TIPO = txtTipo.Text,
                HAB_PRECIO = Convert.ToInt32(txtPrecio.Text),
                HAB_ESTADO = txtEstado.Text,
                HOT_CODIGO = 1
            };

            Models.Habitacion habitacionIngresada =
                await habitacionManager.Ingresar(habitacion, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(habitacionIngresada.HAB_ESTADO))
            {
                lblResultado.Text = "Habitacion ingresada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al ingresar la habitacion";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }

        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            Models.Habitacion habitacion = new Models.Habitacion()
            {
                HAB_CODIGO = Convert.ToInt32(txtHabCod.Text),
                HAB_CANT_HUESP = Convert.ToInt32(txtCantHuesp.Text),
                HAB_TIPO = txtTipo.Text,
                HAB_PRECIO = Convert.ToInt32(txtPrecio.Text),
                HAB_ESTADO = txtEstado.Text,
                HOT_CODIGO = 1
            };

            Models.Habitacion habitacionModificada =
                await habitacionManager.Actualizar(habitacion, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(habitacionModificada.HAB_CODIGO.ToString()))
            {
                lblResultado.Text = "Habitacion modificada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al modificar la habitacion";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {

            string codigoHabitacionEliminada =
                await habitacionManager.Eliminar(txtHabCod.Text,
                VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoHabitacionEliminada))
            {
                lblResultado.Text = "Habitacion eliminada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al eliminar habitacion";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }
    }
}