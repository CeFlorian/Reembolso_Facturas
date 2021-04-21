using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webFacturas.Logica.Negocios;
using webFacturas.Logica.Objetos;

namespace webFacturas
{
    public partial class Inicio : System.Web.UI.Page
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
                            Response.Redirect("/Admin/ListaSolicitudes.aspx", false);
                            return;
                        case (int)clsSistema.Rol.Empleado:
                            Response.Redirect("/Emp/Solicitudes.aspx", false);
                            return;
                    }
                }
                else
                {
                    Response.Redirect("login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx", false);
                Log.GuardarError(this, ex);
            }
        }
    }
}