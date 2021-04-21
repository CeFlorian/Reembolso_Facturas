using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webFacturas.Logica.Objetos;
using webFacturas.Logica.AccesoDatos;

namespace webFacturas.Logica.Negocios
{
    public class clsSistema
    {
        /// <summary>
        /// Tipos de roles que existen actualmente
        /// </summary>
        public enum Rol
        {
            Gerente = 1,
            Empleado = 2
        }


        public enum ResultadoLogin
        {
            ErrorSistema = -1,
            Exitoso = 0,
            EmpleadoNoEcontrado = 1,
            PassIncorrecto = 2,
        }

        clsAccesoDatos accesoDatos;

        public clsSistema()
        {
            accesoDatos = new clsAccesoDatos();
        }


        /// <summary>
        /// Validar usuario que intenta ingresar. Castear Dictionary string, object key: "Nombre", "IdRol"
        /// </summary>
        /// <param name="codigoEmpleado">Código ingresado por el usuario</param>
        /// <param name="pass">Contraseña ingresada por el usuario</param>
        /// <returns></returns>
        public Resultado IngresoSistema(string codigoEmpleado, string pass)
        {
            Resultado resultado = new Resultado();

            try
            {
                ResultadoBD resultadoBd = accesoDatos.ValidarCredenciales(codigoEmpleado);
                switch (resultadoBd.codigoRespuesta)
                {
                    case 0:
                        string passBD = Encriptador.Desencripta(resultadoBd.Datos.Rows[0]["Contrasenia"].ToString());
                        if (passBD.Equals(pass))
                        {
                            //Eliminamos la contraseña para que no se tenga acceso en la capa de aplicación
                            resultadoBd.Datos.Columns.Remove("Contrasenia");

                            resultado.Datos = resultadoBd.Datos;

                            resultado.Exitoso = true;
                            resultado.Mensaje = "Usuario autenticado exitosamente";
                        }
                        else
                        {
                            resultado.Exitoso = false;
                            resultado.Mensaje = "Contraseña incorrecta";
                        }
                        break;
                    case 1:
                        resultado.Exitoso = false;
                        resultado.Mensaje = "Usuario no encontrado";
                        break;
                    default:
                        resultado.Exitoso = false;
                        resultado.Mensaje = "Ocurrió un error al validar su usuario";
                        break;
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.Mensaje = "Error interno al intentar acceder al sistema usuario";
                Log.GuardarError(this, ex);
            }
            return resultado;
        }
    }
}