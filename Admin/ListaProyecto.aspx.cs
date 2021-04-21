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
    public partial class ListaProyecto : System.Web.UI.Page
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

                Resultado resultado = administrador.ConsultaProyectos();

                if (resultado.Exitoso)
                {
                    DataTable usuarios = (DataTable)resultado.Datos;
                    grdViwProyectos.DataSource = usuarios;
                    grdViwProyectos.DataBind();
                    //Agregar header a tabla generada
                    grdViwProyectos.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    string script = "swal('Atención', '" + resultado.Mensaje + "', 'warning')";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                        , script, true);
                    grdViwProyectos.DataSource = null;
                    grdViwProyectos.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
        }

        protected void grdViwProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int linea = int.Parse(e.CommandArgument.ToString());

                    int id = int.Parse(grdViwProyectos.Rows[linea].Cells[0].Text);

                    Resultado resultado = administrador.EliminarProyecto(id);

                    if (resultado.Exitoso)
                    {
                        string script = "swal('Atención', '" + resultado.Mensaje + "', 'success')";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                             , script, true);

                        
                    }
                    else
                    {
                        string script = "swal('Atención', '" + resultado.Mensaje + "', 'error')";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                    }
                    this.LlenarTabla();
                }
            }
            catch (Exception ex)
            {

                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("../ErrorPage.aspx", false);
            }
          
        }

        protected void grdViwProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton boton = (LinkButton)e.Row.Cells[4].Controls[0];
                boton.Attributes.Add("onclick", "confirmarEliminar(event);");
            }
        }
    }
}