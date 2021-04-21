using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webFacturas.Logica.Negocios;
using webFacturas.Logica.Objetos;

namespace webFacturas.Emp
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UsuarioLogueado"] != null && (bool)Session["UsuarioLogueado"] == true)
                {
                    switch ((int)Session["Rol"])
                    {
                        case (int)clsSistema.Rol.Gerente:
                            Response.Redirect("~/Inicio.aspx", false);
                            return;
                    }

                    if (!IsPostBack)
                    {
                        //llenar los datos del empleado barra lateral
                        lbCodigoEmp.Text = (string)Session["codigoEmpleado"];
                        lbUsuario.Text = (string)Session["NombreEmpleado"];

                        this.LlenarTabla();
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

        private void LlenarTabla()
        {
            Empleado empleado = new Empleado();

            Resultado resultado = empleado.ConsultaSolicitudes((int)Session["IdUsuarioLogueado"]);

            if (resultado.Exitoso)
            {
                DataTable solicitudes = (DataTable)resultado.Datos;

                grdViewSolicitudes.DataSource = solicitudes;
                grdViewSolicitudes.DataBind();
                //Agregar header a tabla generada
                grdViewSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            else
            {
                string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'warning'});";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                    , script, true);
            }
        }

        protected void grdViewSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ver")
                {
                    int linea = int.Parse(e.CommandArgument.ToString());

                    int id = int.Parse(grdViewSolicitudes.Rows[linea].Cells[0].Text);

                    Response.Redirect("../Emp/GenerarSolicitud.aspx?Solicitud=" + id.ToString(), false);

                }
            }
            catch (Exception ex)
            {

                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
        }
    }
}