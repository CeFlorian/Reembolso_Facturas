using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webFacturas.Logica.Objetos;
using System.Configuration;

namespace webFacturas.Logica.AccesoDatos
{
    internal class clsAccesoDatos
    {
        private string _connString = string.Empty;

        internal clsAccesoDatos()
        {
            _connString = ConfigurationManager.ConnectionStrings["cadenaConexionBd"].ConnectionString;
        }

        internal ResultadoBD RegistrarUsuario(clsUsuario usuario)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.RegistrarUsuario",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("codigoEmpleado", usuario.codigoEmpleado);
                    cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("Domicilio", usuario.Domicilio);
                    cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("Dpi", usuario.Dpi);
                    cmd.Parameters.AddWithValue("Nit", usuario.Nit);
                    cmd.Parameters.AddWithValue("PlacaAuto", usuario.PlacaAuto);
                    cmd.Parameters.AddWithValue("IdRol", usuario.IdRol);
                    cmd.Parameters.AddWithValue("Contrasenia", usuario.Contrasenia);

                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ConsultarRoles()
        {
            ResultadoBD resultado = new ResultadoBD();

            string querySelect = "SELECT Id, Nombre FROM Rol";

            //Utilizamos sqldataAdapter para hacer select
            using (SqlDataAdapter sqlData = new SqlDataAdapter(querySelect, new SqlConnection(_connString)))
            {
                sqlData.Fill(resultado.Datos);
            }

            //Validamos si encontró al menos un registro
            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                throw new Exception("No se encontró ningún rol");
            }

            return resultado;
        }

        internal ResultadoBD ObtenerEmpleados()
        {

            //Si se ingreso correctamente el empleado = true, si no = false
            ResultadoBD resultado = new ResultadoBD();

            //para guardar los datos que vengan de la bd con los select;
            DataSet datosBD = new DataSet();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ObtenerEmpleados", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(datosBD);
                }
            }

            if (datosBD.Tables[0].Rows.Count > 0)
            {
                resultado.Datos = datosBD.Tables[0];
                resultado.codigoRespuesta = 0;

            }
            else
            {
                resultado.codigoRespuesta = 1;
            }
            return resultado;
        }

        internal ResultadoBD ObtenerEmpleado(int IdUsuario)
        {
            ResultadoBD resultado = new ResultadoBD();

            //para guardar los datos que vengan de la bd con los select;
            DataSet datosBD = new DataSet();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ObtenerEmpleado", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(datosBD);
                }
            }

            if (datosBD.Tables[0].Rows.Count > 0)
            {
                resultado.Datos = datosBD.Tables[0];
                resultado.codigoRespuesta = 0;

            }
            else
            {
                resultado.codigoRespuesta = 1;
            }
            return resultado;
        }

        internal ResultadoBD ActualizarUsuario(clsUsuario usuario)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.ActualizarUsuario",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("IdUsuario", usuario.idUsuario);
                    cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("Domicilio", usuario.Domicilio);
                    cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("Dpi", usuario.Dpi);
                    cmd.Parameters.AddWithValue("Nit", usuario.Nit);
                    cmd.Parameters.AddWithValue("PlacaAuto", usuario.PlacaAuto);
                    cmd.Parameters.AddWithValue("IdRol", usuario.IdRol);
                    cmd.Parameters.AddWithValue("Contrasenia", usuario.Contrasenia);

                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ValidarCredenciales(string codigoEmpleado)
        {

            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ValidarCredenciales", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //Agregamos los parametros para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("codigoEmpleado", codigoEmpleado);
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD EliminarUsuario(int idUsuario)
        {
            int LineasAfectadas = -1;
            ResultadoBD resultado = new ResultadoBD();

            string queryUpdateEliminado = "UPDATE Usuario " +
                                         "SET Eliminado = 1 " +
                                         "WHERE Id = @IdUsuario";

            //Utilizamos sqldataAdapter para hacer select
            using (SqlCommand cmd = new SqlCommand(queryUpdateEliminado, new SqlConnection(_connString)))
            {
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("IdUsuario", idUsuario);
                LineasAfectadas = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

            //Validamos si encontró al menos un registro
            if (LineasAfectadas > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD AgregarProyeto(string NombreProyecto, DateTime FechaInicio, DateTime FechaFin)
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.AgregarProyecto", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //Agregamos los parametros para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", NombreProyecto);
                    cmd.Parameters.AddWithValue("FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("FechaFin", FechaFin);
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ConsultaProyectos()
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ConsultaProyectos", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD EliminarProyecto(int idProyecto)
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.EliminarProyecto", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //Agregamos los parametros para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdProyecto", idProyecto);

                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ConsultaSolicitudes()
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ConsultaFacturasGerente", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }


        internal ResultadoBD ConsultaFacturaConceptos()
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ConsultaFacturaConcepto", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD RegistrarSolicitud(SolicitudFactura solicitudFactura)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.AgregarFactura",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("NoFactura", solicitudFactura.NumeroFactura);
                    cmd.Parameters.AddWithValue("FechaFactura", solicitudFactura.FechaFactura);
                    cmd.Parameters.AddWithValue("IdProyecto", solicitudFactura.IdProyecto);
                    cmd.Parameters.AddWithValue("LugarEmitida", solicitudFactura.LugarEmitido);
                    cmd.Parameters.AddWithValue("Cantidad", solicitudFactura.Cantidad);
                    cmd.Parameters.AddWithValue("MontoTotal", solicitudFactura.MontoTotal);
                    cmd.Parameters.AddWithValue("Descripcion", solicitudFactura.Descripcion);
                    cmd.Parameters.AddWithValue("IdConcepto", solicitudFactura.IdConcepto);
                    cmd.Parameters.AddWithValue("IdUsuario", solicitudFactura.IdUsuario);


                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ConsultaSolicitudesEmpleado(int IdUsuario)
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ConsultaFacturasEmpleado", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD ConsultaDetalleSolicitudeEmpleado(int IdSolicitud, int IdUsuario)
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.ConsultaDetalleFacturaEmpleado", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                cmd.Parameters.AddWithValue("IdFactura", IdSolicitud);
                cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD ModificarSolicitud(SolicitudFactura solicitudFactura)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.ModificarFactura",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("IdFactura", solicitudFactura.IdSolicitud);
                    cmd.Parameters.AddWithValue("NoFactura", solicitudFactura.NumeroFactura);
                    cmd.Parameters.AddWithValue("FechaFactura", solicitudFactura.FechaFactura);
                    cmd.Parameters.AddWithValue("IdProyecto", solicitudFactura.IdProyecto);
                    cmd.Parameters.AddWithValue("LugarEmitida", solicitudFactura.LugarEmitido);
                    cmd.Parameters.AddWithValue("Cantidad", solicitudFactura.Cantidad);
                    cmd.Parameters.AddWithValue("MontoTotal", solicitudFactura.MontoTotal);
                    cmd.Parameters.AddWithValue("Descripcion", solicitudFactura.Descripcion);
                    cmd.Parameters.AddWithValue("IdConcepto", solicitudFactura.IdConcepto);

                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD EliminarSolicitud(int IdSolicitud)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.[EliminarFactura]",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("IdFactura", IdSolicitud);

                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

        internal ResultadoBD ConsultaDetalleSolicitudGerente(int IdSolicitud)
        {
            //Objeto que se devolverá al objeto que esta llamando a este método
            ResultadoBD resultado = new ResultadoBD();

            DataTable datosBD = new DataTable();

            //Para ejecutar el procedimiento almacenado
            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure, //colocamos que tipo de comando es
                CommandText = "dbo.[ConsultaDetalleFacturaGerente]", //colocamos el nombre del procedimiento almacenado
                Connection = new SqlConnection(_connString) // la conexión a BD

            })
            {
                cmd.Parameters.AddWithValue("IdFactura", IdSolicitud);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    //comando para llenar el dataset con los resultados de la operación
                    SqlData.Fill(resultado.Datos);
                }
            }

            if (resultado.Datos.Rows.Count > 0)
            {
                resultado.codigoRespuesta = 0;
            }
            else
            {
                resultado.codigoRespuesta = 1;
            }

            return resultado;
        }

        internal ResultadoBD AtenderSolicitud(int IdSolicitud, int IdEstado)
        {
            DataTable datosBD = new DataTable();
            ResultadoBD resultado = new ResultadoBD();

            using (SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.[AtenderFactura]",
                Connection = new SqlConnection(_connString)
            })
            {
                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("IdFactura", IdSolicitud);
                    cmd.Parameters.AddWithValue("EstadoNuevo", IdEstado);

                    SqlData.Fill(datosBD);

                    resultado.codigoRespuesta = int.Parse(datosBD.Rows[0]["ReturnCode"].ToString());

                }
            }

            return resultado;
        }

    }
}