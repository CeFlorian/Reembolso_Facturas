using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webFacturas.Logica.Negocios;
using webFacturas.Logica.Objetos;

namespace webFacturas.Admin
{
    public partial class DetalleSolicitud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioLogueado"] != null && (bool)Session["UsuarioLogueado"] == true)
                {
                    switch ((int)Session["Rol"])
                    {
                        case (int)clsSistema.Rol.Empleado:
                            Response.Redirect("../Inicio.aspx", false);
                            return;
                    }

                    if (!IsPostBack)
                    {
                        lbCodigoEmp.Text = (string)Session["codigoEmpleado"];
                        lbUsuario.Text = (string)Session["NombreEmpleado"];

                        if (Request.QueryString["Solicitud"] != null)
                        {
                            int IdSolicitud = int.Parse(Request.QueryString["Solicitud"].ToString());

                            this.LlenarDatos(IdSolicitud);
                        }
                    }
                }
                else
                {
                    Response.Redirect("../login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../ErrorPage.aspx", false);
                Log.GuardarError(this, ex);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Administrador administrador = new Administrador();

                Resultado resultado = administrador.AtenderSolicitud((int)Session["IdSolicitudActual"], 3);

                if (resultado.Exitoso)
                {
                    string script = "swal({title: 'Atención',text: 'La solicitud a sido rechazada',type: 'success'}, function() {window.location = '../Admin/ListaSolicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                }
                else
                {
                    string script = "swal({title: 'Atención',text: '"+ resultado.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaSolicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                }
            }
            catch (Exception ex)
            {

                Response.Redirect("../ErrorPage.aspx", false);
                Log.GuardarError(this, ex);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Administrador administrador = new Administrador();

                Resultado resultado = administrador.AtenderSolicitud((int)Session["IdSolicitudActual"], 2);

                if (resultado.Exitoso)
                {
                    string script = "swal({title: 'Atención',text: 'La solicitud a sido aprobada',type: 'success'}, function() {window.location = '../Admin/ListaSolicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                }
                else
                {
                    string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaSolicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                }
            }
            catch (Exception ex)
            {

                Response.Redirect("../ErrorPage.aspx", false);
                Log.GuardarError(this, ex);
            }
        }

        private void LlenarDatos(int IdSolicitud)
        {
            Administrador administrador = new Administrador();

            Resultado resultado = administrador.ConsultaDetalleSolicitud(IdSolicitud);

            if (resultado.Exitoso)
            {
                DataTable detalleSolicitud = new DataTable();
                detalleSolicitud = (DataTable)resultado.Datos;

                Session["IdSolicitudActual"] = int.Parse(detalleSolicitud.Rows[0]["Solicitud"].ToString());
                txtCodEmpleado.Text = detalleSolicitud.Rows[0]["CodEmpleado"].ToString();
                txtNombreEmpleado.Text = detalleSolicitud.Rows[0]["NombreEmpleado"].ToString();
                txtNombreProyecto.Text = detalleSolicitud.Rows[0]["NombreProyecto"].ToString();
                txtConcepto.Text = detalleSolicitud.Rows[0]["Concepto"].ToString();
                txtCantidad.Text = detalleSolicitud.Rows[0]["Cantidad"].ToString();
                txtMonto.Text = detalleSolicitud.Rows[0]["MontoTotal"].ToString();
                txtFecha.Text = detalleSolicitud.Rows[0]["FechaFactura"].ToString();
                txtDescripcion.Text = detalleSolicitud.Rows[0]["Descripcion"].ToString();
                txtNoFactura.Text = detalleSolicitud.Rows[0]["NoFactura"].ToString();
                txtEstado.Text = detalleSolicitud.Rows[0]["Estado"].ToString();
            }
            else
            {
                string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaSolicitudes.aspx';});";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
            }
        }
    }
}