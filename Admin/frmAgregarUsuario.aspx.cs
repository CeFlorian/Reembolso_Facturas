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
    public partial class frmAgregarUsuario : System.Web.UI.Page
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
                            break;
                    }

                    if (!IsPostBack)
                    {
                        lbCodigoEmp.Text = (string)Session["codigoEmpleado"];
                        lbUsuario.Text = (string)Session["NombreEmpleado"];

                        Resultado resultadoRoles = administrador.LlenarRoles();
                        if (resultadoRoles.Exitoso == true)
                        {
                            DataTable roles = (DataTable)resultadoRoles.Datos;
                            foreach (DataRow rol in roles.Rows)
                            {
                                ddListPuesto.Items.Add(new ListItem() { Value = rol["Id"].ToString(), Text = rol["Nombre"].ToString() });
                            }
                        }
                        else
                        {
                            Response.Redirect("../ErrorPage.aspx", false);
                        }

                        if (Request.QueryString["Editar"] != null && Request.QueryString["Editar"].ToString().Equals("1"))
                        {
                            int idUsuario = (int)Session["IdUsuario"];
                            Session["EsEdicion"] = true;
                            this.LLenarCampos(idUsuario);
                            btnRegistrar.Text = "Modificar";
                            btnEliminar.Visible = true;
                        }
                        else
                        {
                            Session["EsEdicion"] = false;
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


        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["EsEdicion"] != null && (bool)Session["EsEdicion"])
                {
                    clsUsuario usuario = new clsUsuario()
                    {
                        idUsuario = (int)Session["IdUsuario"],
                        Nombres = txtNombres.Text,
                        Apellidos = txtApellidos.Text,
                        Domicilio = txtDomicilio.Text,
                        Telefono = txtTelefono.Text,
                        Dpi = txtDpi.Text,
                        Nit = txtNit.Text,
                        PlacaAuto = txtPlacaAuto.Text,
                        Contrasenia = txtContrasenia.Text,
                        IdRol = int.Parse(ddListPuesto.SelectedValue)
                    };

                    Resultado resultadoActulizacion = administrador.ActualizarUsuario(usuario);

                    if (resultadoActulizacion.Exitoso == true)
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoActulizacion.Mensaje + "',type: 'success'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                             , script, true);
                    }
                    else
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoActulizacion.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                    }
                }
                else
                {
                    clsUsuario usuario = new clsUsuario()
                    {
                        codigoEmpleado = txtCodigoEmpleado.Text,
                        Nombres = txtNombres.Text,
                        Apellidos = txtApellidos.Text,
                        Domicilio = txtDomicilio.Text,
                        Telefono = txtTelefono.Text,
                        Dpi = txtDpi.Text,
                        Nit = txtNit.Text,
                        PlacaAuto = txtPlacaAuto.Text,
                        Contrasenia = txtContrasenia.Text,
                        IdRol = int.Parse(ddListPuesto.SelectedValue)
                    };

                    Resultado resultadoRegistro = administrador.RegistrarUsuario(usuario);

                    if (resultadoRegistro.Exitoso == true)
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoRegistro.Mensaje + "',type: 'success'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                             , script, true);
                    }
                    else
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoRegistro.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                    }
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
                Resultado resultadoEliminar = administrador.EliminarUsuario((int)Session["IdUsuario"]);

                if (resultadoEliminar.Exitoso == true)
                {
                    string script = "swal({title: 'Atención',text: '" + resultadoEliminar.Mensaje + "',type: 'success'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                         , script, true);
                }
                else
                {
                    string script = "swal({title: 'Atención',text: '" + resultadoEliminar.Mensaje + "',type: 'error'}, function() {window.location = '../Admin/ListaUsuarios.aspx';});";
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


        protected void LLenarCampos(int idUsuario)
        {

            Resultado resultadoConsulta = administrador.ConsultarUsuarios(idUsuario);

            if (resultadoConsulta.Exitoso == true)
            {
                clsUsuario usuario = (clsUsuario)resultadoConsulta.Datos;
                txtCodigoEmpleado.Text = usuario.codigoEmpleado;
                txtCodigoEmpleado.Enabled = false;
                txtNombres.Text = usuario.Nombres;
                txtApellidos.Text = usuario.Apellidos;
                txtDpi.Text = usuario.Dpi;
                txtNit.Text = usuario.Nit;
                txtTelefono.Text = usuario.Telefono;
                txtDomicilio.Text = usuario.Domicilio;
                txtPlacaAuto.Text = usuario.PlacaAuto;
                txtContrasenia.Attributes.Add("value", usuario.Contrasenia);
                ddListPuesto.SelectedValue = usuario.IdRol.ToString();
            }
        }
    }
}