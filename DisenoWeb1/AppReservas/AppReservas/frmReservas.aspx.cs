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
    public partial class frmReservas : System.Web.UI.Page
    {
        ReservaManager reservaManager = new ReservaManager();
        IEnumerable<Models.Reserva> reservas = new ObservableCollection<Models.Reserva>();

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
            reservas = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);
            grdHoteles.DataSource = reservas.ToList();
            grdHoteles.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Models.Reserva reserva = new Models.Reserva()
            {
                RES_FECHA = DateTime.Now,
                RES_FECHA_INGRESO = Convert.ToDateTime(txtFechaIngreso.Text),
                RES_FECHA_SALIDA = Convert.ToDateTime(txtFechaSalida.Text),
                RES_ESTADO = txtEstado.Text.ToString(),
                USU_CODIGO = Convert.ToInt32(txtUsuCod.Text),
                HAB_CODIGO = Convert.ToInt32(txtHabCodigo.Text),
            };

            Models.Reserva reservaIngresada =
                await reservaManager.Ingresar(reserva, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(reservaIngresada.RES_FECHA.ToString()) || !string.IsNullOrEmpty(reservaIngresada.RES_FECHA_INGRESO.ToString()) || !string.IsNullOrEmpty(reservaIngresada.RES_FECHA_SALIDA.ToString()))
            {
                lblResultado.Text = "Reserva ingresada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al ingresar la reserva";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }

        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {

            Models.Reserva reserva = new Models.Reserva()
            {
                RES_CODIGO = Convert.ToInt32(txtReservaCod.Text),
                RES_FECHA = DateTime.Now,
                RES_FECHA_INGRESO = Convert.ToDateTime(txtFechaIngreso.Text),
                RES_FECHA_SALIDA = Convert.ToDateTime(txtFechaSalida.Text),
                RES_ESTADO = txtEstado.Text.ToString(),
                USU_CODIGO = Convert.ToInt32(txtUsuCod.Text),
                HAB_CODIGO = Convert.ToInt32(txtHabCodigo.Text),
            };

            Models.Reserva reservaModificada =
                await reservaManager.Actualizar(reserva, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(reservaModificada.RES_FECHA.ToString()) || !string.IsNullOrEmpty(reservaModificada.RES_FECHA_INGRESO.ToString()) || !string.IsNullOrEmpty(reservaModificada.RES_FECHA_SALIDA.ToString()))
            {
                lblResultado.Text = "Reserva modificada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al modificar la reserva";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {

            string codigoReservaEliminada =
                await reservaManager.Eliminar(txtReservaCod.Text,
                VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoReservaEliminada))
            {
                lblResultado.Text = "Reserva eliminada exitosamente";
                lblResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Error al eliminar la reserva";
                lblResultado.ForeColor = Color.Red;
                lblResultado.Visible = true;
            }
        }
    }
}