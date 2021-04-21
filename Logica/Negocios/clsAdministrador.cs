using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webFacturas.Logica.Objetos;
using webFacturas.Logica.AccesoDatos;

namespace webFacturas.Logica.Negocios
{
    public class Administrador
    {

        public Resultado RegistrarUsuario(clsUsuario usuario)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                usuario.Contrasenia = Encriptador.Encripta(usuario.Contrasenia);

                ResultadoBD resultadoBD = accesoDatos.RegistrarUsuario(usuario);

                switch (resultadoBD.codigoRespuesta)
                {
                    case 0:
                        resultado.Exitoso = true;
                        resultado.Mensaje = "Usuario registrado correctamente";
                        break;
                    case 1:
                        resultado.Exitoso = false;
                        resultado.Mensaje = "Usuario no registrado. El codigo de empleado ingresado ya existe.";
                        break;
                    default:
                        resultado.Exitoso = false;
                        resultado.Mensaje = "Usuario no registrado. Ocurrió un error al intentar registrar el usuario.";
                        break;
                }

            }
            catch (Exception ex)
            {
                resultado.Exitoso= false;
                resultado.Mensaje = "Error interno al intentar registrar usuario";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado LlenarRoles()
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                ResultadoBD resultadoBD = accesoDatos.ConsultarRoles();

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Datos = resultadoBD.Datos;
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No existen roles";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al consultar roles";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado ConsultarUsuarios()
        {
            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            Resultado resultado = new Resultado();

            try
            {
                ResultadoBD resultadoBd = accesoDatos.ObtenerEmpleados();

                if (resultadoBd.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Datos = resultadoBd.Datos;
                }
                else if (resultadoBd.codigoRespuesta == 1)
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No hay usuario registrados";
                }
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al consultar usuarios";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado ConsultarUsuarios(int idUsuario)
        {
            Resultado resultado = new Resultado();
            clsUsuario usuario = new clsUsuario();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                ResultadoBD resultadoBD = accesoDatos.ObtenerEmpleado(idUsuario);
                if (resultadoBD.codigoRespuesta == 0)
                {
                    usuario.codigoEmpleado = resultadoBD.Datos.Rows[0]["CodigoEmpleado"].ToString();
                    usuario.Nombres = resultadoBD.Datos.Rows[0]["Nombres"].ToString();
                    usuario.Apellidos = resultadoBD.Datos.Rows[0]["Apellidos"].ToString();
                    usuario.Dpi = resultadoBD.Datos.Rows[0]["Dpi"].ToString();
                    usuario.Nit = resultadoBD.Datos.Rows[0]["Nit"].ToString();
                    usuario.Telefono = resultadoBD.Datos.Rows[0]["Telefono"].ToString();
                    usuario.Domicilio = resultadoBD.Datos.Rows[0]["Domicilio"].ToString();
                    usuario.PlacaAuto = resultadoBD.Datos.Rows[0]["Placa"].ToString();
                    usuario.Contrasenia =Encriptador.Desencripta(resultadoBD.Datos.Rows[0]["Pass"].ToString());
                    usuario.IdRol = int.Parse(resultadoBD.Datos.Rows[0]["IdRol"].ToString());

                    resultado.Exitoso = true;
                    resultado.Datos = usuario;
                }
                else if (resultadoBD.codigoRespuesta == 1)
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se encontró ningún usuario con este id.";
                }
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Ocurrió un error al intentar consulta usuario.";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado ActualizarUsuario(clsUsuario usuario)
        {
            Resultado resultado = new Resultado();

            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {
                usuario.Contrasenia = Encriptador.Encripta(usuario.Contrasenia);

                ResultadoBD resultadoBD = accesoDatos.ActualizarUsuario(usuario);

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Usuario actualizado exitosamente";
                }
                else if (resultadoBD.codigoRespuesta == 1)
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "Usuario no registrado. El código de empleado ya existe. Favor usar otro código.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Ocurrió un error al intentar actualizar el usuario.";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado EliminarUsuario(int idUsuario)
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.EliminarUsuario(idUsuario);

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Usuario eliminado correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo eliminar el usuario.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar actualizar usuario";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado AgregarProyecto(string Nombre, DateTime FechaInicio, DateTime FechaFin)
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.AgregarProyeto(Nombre, FechaInicio, FechaFin);

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Proyecto agregado correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo agregar el proyecto.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar agregar el proyecto";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado ConsultaProyectos()
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.ConsultaProyectos();

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Datos = resultadoBD.Datos;
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Proyectos consultados correctamente";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No hay proyectos para mostrar.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar consultar proyectos";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }


        public Resultado EliminarProyecto(int IdProyecto)
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.EliminarProyecto(IdProyecto);

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Proyecto eliminado correctamente.";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se pudo eliminar el proyecto.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar eliminar el proyecto";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado ConsultaSolicitudes()
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.ConsultaSolicitudes();

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Datos = resultadoBD.Datos;
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Solicitudes consultadas correctamente";
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No hay solicitudes para mostrar.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar consultar solicitudes";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }

        public Resultado ConsultaDetalleSolicitud(int IdSolicitud)
        {
            clsAccesoDatos accesoDatos = new clsAccesoDatos();

            Resultado resultado = new Resultado();

            try
            {
                ResultadoBD resultadoBd = accesoDatos.ConsultaDetalleSolicitudGerente(IdSolicitud);

                if (resultadoBd.codigoRespuesta == 0)
                {
                    resultado.Datos = resultadoBd.Datos;
                    resultado.Exitoso = true;
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "No se encontró la solicitud";
                }
            }
            catch (Exception ex)
            {

                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al consultar la solicitud";
                Log.GuardarError(this, ex);
            }

            return resultado;
        }

        public Resultado AtenderSolicitud(int IdSolicitud, int IdEstado)
        {
            Resultado resultado = new Resultado();
            clsAccesoDatos accesoDatos = new clsAccesoDatos();
            try
            {

                ResultadoBD resultadoBD = accesoDatos.AtenderSolicitud(IdSolicitud, IdEstado);

                if (resultadoBD.codigoRespuesta == 0)
                {
                    resultado.Exitoso = true;
                    resultado.Mensaje = "Operación correcta.";
                }
                else if(resultadoBD.codigoRespuesta == 1)
                {
                    resultado.Exitoso = false;
                    resultado.Mensaje = "Solicitud eliminada.";
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar atender la solicitud";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }
    }
}