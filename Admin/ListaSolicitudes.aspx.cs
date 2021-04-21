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
    public partial class ListaSolicitudes : System.Web.UI.Page
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
                        this.LlenarTabla();
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

                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
        }

        protected void LlenarTabla()
        {
            try
            {

                Resultado resultado = administrador.ConsultaSolicitudes();

                if (resultado.Exitoso)
                {
                    DataTable usuarios = (DataTable)resultado.Datos;
                    grdViewSolicitudes.DataSource = usuarios;
                    grdViewSolicitudes.DataBind();
                    //Agregar header a tabla generada
                    grdViewSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    string script = "swal('Atención', '" + resultado.Mensaje + "', 'warning')";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                        , script, true);
                    grdViewSolicitudes.DataSource = null;
                    grdViewSolicitudes.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
        }

        protected void grdViewSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Atender")
                {
                    int linea = int.Parse(e.CommandArgument.ToString());

                    int id = int.Parse(grdViewSolicitudes.Rows[linea].Cells[0].Text);

                    Response.Redirect("../Admin/DetalleSolicitud.aspx?Solicitud=" + id.ToString(), false);

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