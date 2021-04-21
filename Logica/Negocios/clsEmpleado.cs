using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webFacturas.Logica.AccesoDatos;
using webFacturas.Logica.Objetos;

namespace webFacturas.Logica.Negocios
{
    public class Empleado
    {
        public Resultado ConsultaProyectos()
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                ResultadoBD resultadoBD = accesoDatos.ConsultaProyectos();

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Datos = resultadoBD.Datos;
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No existen proyectos";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al consultar proyectos";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado ConsultaConceptosFactura()
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                ResultadoBD resultadoBD = accesoDatos.ConsultaFacturaConceptos();

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Datos = resultadoBD.Datos;
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No existen conceptos de factura";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al consultar concepto de factura";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado RegistrarFactura(SolicitudFactura solicitudFactura)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            try
            {
                ResultadoBD resultadoBD = accesoDatos.RegistrarSolicitud(solicitudFactura);

                if (resultadoBD.codigoRespuesta.Equals(0))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Se ha generado la solicitud de reembolso correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo generar la solicitud de reembolso.";
                }
                return resultado;
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar generar la solicitud de reembolso.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado ConsultaSolicitudes(int IdUsuario)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            try
            {
                ResultadoBD resultadoBD = accesoDatos.ConsultaSolicitudesEmpleado(IdUsuario);

                if (resultadoBD.codigoRespuesta.Equals(0))
                {
                    resultado.Datos = resultadoBD.Datos;
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Consulta Existosa";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No hay solicitudes para este usuario";
                }
                return resultado;
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar consultar las solicitudes de este usuario.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado ConsultaDetalleSolicitud(int IdUsuario, int IdSolicitud)
        {
            Resultado resultado = new Resultado();
            SolicitudFactura solicitudFactura = new SolicitudFactura();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();



            try
            {
                ResultadoBD resultadoBD = accesoDatos.ConsultaDetalleSolicitudeEmpleado(IdSolicitud,IdUsuario);

                if (resultadoBD.codigoRespuesta.Equals(0))
                {
                    solicitudFactura.IdSolicitud = int.Parse(resultadoBD.Datos.Rows[0]["Id"].ToString());
                    solicitudFactura.NumeroFactura= resultadoBD.Datos.Rows[0]["NoFactura"].ToString();
                    solicitudFactura.FechaFactura = DateTime.Parse(resultadoBD.Datos.Rows[0]["FechaFactura"].ToString());
                    solicitudFactura.IdProyecto = int.Parse(resultadoBD.Datos.Rows[0]["IdProyecto"].ToString());
                    solicitudFactura.LugarEmitido = resultadoBD.Datos.Rows[0]["LugarEmitida"].ToString();
                    solicitudFactura.Cantidad = int.Parse(resultadoBD.Datos.Rows[0]["Id"].ToString());
                    solicitudFactura.MontoTotal = double.Parse(resultadoBD.Datos.Rows[0]["MontoTotal"].ToString());
                    solicitudFactura.Descripcion = resultadoBD.Datos.Rows[0]["Descripcion"].ToString();
                    solicitudFactura.IdConcepto = int.Parse(resultadoBD.Datos.Rows[0]["IdConcepto"].ToString());
                    solicitudFactura.IdEstado = int.Parse(resultadoBD.Datos.Rows[0]["IdEstado"].ToString());

                    resultado.Datos = solicitudFactura;
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Consulta Existosa";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo recuperar los detalles de la solicitud.";
                }
                return resultado;
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar consultar las los detalles de la solicitud.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado ModificarSolicitud(SolicitudFactura solicitudFactura)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            try
            {
                ResultadoBD resultadoBD = accesoDatos.ModificarSolicitud(solicitudFactura);

                if (resultadoBD.codigoRespuesta.Equals(0))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Se ha modificado la solicitud de reembolso correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo modificar la solicitud de reembolso.";
                }
                return resultado;
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar modificar la solicitud de reembolso.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado EliminarSolicitud(int idSolicitud)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            try
            {
                ResultadoBD resultadoBD = accesoDatos.EliminarSolicitud(idSolicitud);

                if (resultadoBD.codigoRespuesta.Equals(0))
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Se eliminó la solicitud de reembolso correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo eliminar la solicitud de reembolso.";
                }
                return resultado;
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar eliminar la solicitud de reembolso.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }
    }
}