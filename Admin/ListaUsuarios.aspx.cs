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
    public partial class ListaUsuarios : System.Web.UI.Page
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

                Resultado resultado = administrador.ConsultarUsuarios();

                if (resultado.Exitoso)
                {
                    DataTable usuarios = (DataTable)resultado.Datos;
                    grdViwUsuario.DataSource = usuarios;
                    grdViwUsuario.DataBind();
                    //Agregar header a tabla generada
                    grdViwUsuario.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    string script = "alert('" + resultado.Mensaje + "')";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                    grdViwUsuario.DataSource = null;
                    grdViwUsuario.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
        }

        protected void grdViwUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["IdUsuario"] = int.Parse(grdViwUsuario.SelectedRow.Cells[0].Text);
                Response.Redirect("frmAgregarUsuario.aspx?Editar=1", false);
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