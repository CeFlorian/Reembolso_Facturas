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
    public partial class frmAgregarProyecto : System.Web.UI.Page
    {

        Administrador administrador = new Administrador();
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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                administrador = new Administrador();

                Resultado resultadoAgregarProyecto = administrador.AgregarProyecto(txtNombreEmpleado.Text, 
                    DateTime.Parse(txtFechaInicio.Text), DateTime.Parse(txtFechaFin.Text));

                if (resultadoAgregarProyecto.Exitoso)
                {
                    string script = "swal({title: 'Atención',text: '" + resultadoAgregarProyecto.Mensaje + "',type: 'success'}, function() {window.location = '../Admin/ListaProyecto.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                        , script, true);
                }
                else
                {
                    string script = "swal({title: 'Atención',text: '" + resultadoAgregarProyecto.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaProyecto.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                        , script, true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../ErrorPage.aspx", false);
                Log.GuardarError(this, ex);
            }

        }
    }
}