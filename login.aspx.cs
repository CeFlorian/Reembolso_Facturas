using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webFacturas.Logica.Negocios;
using webFacturas.Logica.Objetos;

namespace webFacturas
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                clsSistema sistema = new clsSistema();

                Resultado resultado = sistema.IngresoSistema(txtUsuario.Text, txtPass.Text);
                if (resultado.Exitoso)
                {
                    DataTable datos = (DataTable)resultado.Datos;
                    Session["codigoEmpleado"] = datos.Rows[0]["CodigoEmpleado"].ToString();
                    Session["NombreEmpleado"] = datos.Rows[0]["Nombre"].ToString();
                    Session["Rol"] = int.Parse(datos.Rows[0]["IdRol"].ToString());
                    Session["IdUsuarioLogueado"] = int.Parse(datos.Rows[0]["IdUsuarioLogueado"].ToString());
                    Session["UsuarioLogueado"] = true;
                    Response.Redirect("Inicio.aspx", false);
                }
                else
                {
                    Session["UsuarioLogueado"] = false;
                    lbMensaje.Text = resultado.Mensaje;
                    lbMensaje.Visible = true;
                }

            }
            catch (Exception ex)
            {
                //Si se produce un error guardamos un log
                Log.GuardarError(this, ex);
                Response.Redirect("ErrorPage.aspx", false);
            }
            

        }
    }
}