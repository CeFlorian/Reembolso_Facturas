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
    public partial class GenerarSolicitud : System.Web.UI.Page
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

                        //llenar datos del empleado
                        this.LlenarDatosNuevo();

                        //llenar dropdownlist
                        this.LlenarDrops();

                        if (Request.QueryString["Solicitud"] != null && !Request.QueryString["Solicitud"].ToString().Equals("0"))
                        {
                            int IdSolicitud = int.Parse(Request.QueryString["Solicitud"].ToString());
                            Session["SolicitudActual"] = IdSolicitud;
                            Session["EsEdicion"] = true;
                            btnRegistrar.Text = "Modificar";
                            btnEliminar.Visible = true;
                            DivEstado.Visible = true;

                            this.LlenarDetalleSolicitud((int)Session["IdUsuarioLogueado"], IdSolicitud);
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

        private void LlenarDrops()
        {
            Empleado empleado = new Empleado();

            //Llenar dropdownlist de proyectos
            Resultado resultadoConsulta = empleado.ConsultaProyectos();

            if (resultadoConsulta.Exitoso)
            {
                DataTable proyectos = (DataTable)resultadoConsulta.Datos;

                ddListProyecto.Items.Add(new ListItem() { Text = "Seleccione un proyecto", Value = "0" });

                foreach (DataRow proyecto in proyectos.Rows)
                {
                    ddListProyecto.Items.Add(new ListItem() { 
                        Text = proyecto["NombreProyecto"].ToString(), 
                        Value = proyecto["Id"].ToString() });
                }
                ddListProyecto.DataBind();
            }
            else
            {
                string script = "swal({title: 'Atención',text: '" + resultadoConsulta.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                    , script, true);
            }

            //llenar dropodownlist de conceptos de la factura
            resultadoConsulta = empleado.ConsultaConceptosFactura();

            if(resultadoConsulta.Exitoso)
            {
                DataTable proyectos = (DataTable)resultadoConsulta.Datos;

                ddListConcepto.Items.Add(new ListItem() { Text = "Seleccione un concepto", Value = "0" });

                foreach (DataRow proyecto in proyectos.Rows)
                {
                    ddListConcepto.Items.Add(new ListItem()
                    {
                        Text = proyecto["Descripcion"].ToString(),
                        Value = proyecto["Id"].ToString()
                    });
                }
                ddListConcepto.DataBind();
            }
            else
            {
                string script = "swal({title: 'Atención',text: '" + resultadoConsulta.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                    , script, true);
            }

        }

        private void LlenarDatosNuevo()
        {
            //llenar datos del empleado
            txtCodEmpleado.Text = (string)Session["codigoEmpleado"];
            txtNombreEmpleado.Text = (string)Session["NombreEmpleado"];
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado empleado = new Empleado();

                //Verificar si se esta editando la solicitud
                if (!(bool)Session["EsEdicion"])
                {
                    //Verificar que todos los datos esten completos

                    if (ddListProyecto.SelectedValue.Equals("0"))
                    {
                        string script = "swal({title: 'Atención',text: 'Seleccione el proyecto en el que se encuentra',type: 'warning'});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                    }
                    else if (ddListConcepto.SelectedValue.Equals("0"))
                    {
                        string script = "swal({title: 'Atención',text: 'Seleccione el concepto de la factura',type: 'warning'});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                    }
                    else
                    {
                        //Llenado de objeto para enviar
                        SolicitudFactura solicitudFactura = new SolicitudFactura();

                        solicitudFactura.NumeroFactura = txtNoFactura.Text;
                        solicitudFactura.FechaFactura = DateTime.Parse(txtFecha.Text);
                        solicitudFactura.IdProyecto = int.Parse(ddListProyecto.SelectedValue);
                        solicitudFactura.LugarEmitido = txtLugarEmitido.Text;
                        solicitudFactura.Cantidad = int.Parse(txtCantidad.Text);
                        solicitudFactura.MontoTotal = double.Parse(txtMonto.Text);
                        solicitudFactura.Descripcion = txtDescripcion.Text;
                        solicitudFactura.IdConcepto = int.Parse(ddListConcepto.SelectedValue);
                        solicitudFactura.IdUsuario = (int)Session["IdUsuarioLogueado"];

                        //Ejecutamos el metodo
                        Resultado resultadoGeneracionSolicitud = empleado.RegistrarFactura(solicitudFactura);

                        if (resultadoGeneracionSolicitud.Exitoso)
                        {
                            string script = "swal({title: 'Atención',text: '" + resultadoGeneracionSolicitud.Mensaje + "',type: 'success'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                        }
                        else
                        {
                            string script = "swal({title: 'Atención',text: '" + resultadoGeneracionSolicitud.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                            , script, true);
                        }
                    }

                }
                else
                {
                    //Llenado de objeto para enviar
                    SolicitudFactura solicitudFactura = new SolicitudFactura();

                    solicitudFactura.IdSolicitud = (int)Session["SolicitudActual"];
                    solicitudFactura.NumeroFactura = txtNoFactura.Text;
                    solicitudFactura.FechaFactura = DateTime.Parse(txtFecha.Text);
                    solicitudFactura.IdProyecto = int.Parse(ddListProyecto.SelectedValue);
                    solicitudFactura.LugarEmitido = txtLugarEmitido.Text;
                    solicitudFactura.Cantidad = int.Parse(txtCantidad.Text);
                    solicitudFactura.MontoTotal = double.Parse(txtMonto.Text);
                    solicitudFactura.Descripcion = txtDescripcion.Text;
                    solicitudFactura.IdConcepto = int.Parse(ddListConcepto.SelectedValue);
                    //Ejecutamos el metodo
                    Resultado resultadoModificacionSolicitud = empleado.ModificarSolicitud(solicitudFactura);

                    if (resultadoModificacionSolicitud.Exitoso)
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoModificacionSolicitud.Mensaje + "',type: 'success'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje"
                        , script, true);
                    }
                    else
                    {
                        string script = "swal({title: 'Atención',text: '" + resultadoModificacionSolicitud.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
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

        private void LlenarDetalleSolicitud(int IdUsuario, int IdSolicitud)
        {
            Empleado empleado = new Empleado();

            Resultado resultado = empleado.ConsultaDetalleSolicitud(IdUsuario, IdSolicitud);

            if (resultado.Exitoso)
            {
                SolicitudFactura solicitudFactura = (SolicitudFactura)resultado.Datos;

                txtCodEmpleado.Text = (string)Session["codigoEmpleado"];
                txtNombreEmpleado.Text = (string)Session["NombreEmpleado"];
                txtFecha.Text = solicitudFactura.FechaFactura.ToString("dd/MM/yyyy");
                ddListProyecto.SelectedValue = solicitudFactura.IdProyecto.ToString();
                ddListConcepto.SelectedValue = solicitudFactura.IdConcepto.ToString();
                txtNoFactura.Text = solicitudFactura.NumeroFactura;
                txtLugarEmitido.Text = solicitudFactura.LugarEmitido;
                txtCantidad.Text = solicitudFactura.Cantidad.ToString();
                txtDescripcion.Text = solicitudFactura.Descripcion;
                txtMonto.Text = solicitudFactura.MontoTotal.ToString();

                switch (solicitudFactura.IdEstado)
                {
                    case 1:
                        txtEstado.Text = "Sin Atender";
                        break;
                    case 2:
                        txtEstado.Text = "Probada";
                        break;
                    case 3:
                        txtEstado.Text = "Rechazada";
                        break;
                }

                if (solicitudFactura.IdEstado != 1)
                {
                    txtFecha.Enabled = false;
                    ddListConcepto.Enabled = false;
                    ddListProyecto.Enabled = false;
                    txtLugarEmitido.Enabled = false;
                    txtNoFactura.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtDescripcion.Enabled = false;

                    btnRegistrar.Enabled = false;
                    btnEliminar.Enabled = false;
                }

            }
            else
            {
                string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado empleado = new Empleado();

                Resultado resultado = empleado.EliminarSolicitud((int)Session["SolicitudActual"]);

                if (resultado.Exitoso)
                {
                    string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'success'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
                }
                else
                {
                    string script = "swal({title: 'Atención',text: '" + resultado.Mensaje + "',type: 'error'}, function() {window.location = '../Emp/Solicitudes.aspx';});";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ejecutarMensaje", script, true);
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