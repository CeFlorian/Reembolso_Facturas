using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace webFacturas.Logica.Objetos
{
    public class Encriptador
    {
        private static byte[] Clave = Encoding.ASCII.GetBytes("IngenieriaS0f");
        private static byte[] IV = Encoding.ASCII.GetBytes("Dev67.IngS0ft*20");

        static public string Encripta(string Cadena)
        {

            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }



        static public string Desencripta(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
    }

    public class Log
    {
        public static void GuardarError(object obj, Exception ex)
        {
            StackTrace stacktrace = new StackTrace();
            Log log = new Log();

            if (!log.RegistrarEnBD(obj.GetType().FullName, stacktrace.GetFrame(1).GetMethod().Name, ex.Message, ex.StackTrace))
            {
                RegistrarFile(obj, ex);
            }
        }

        private bool RegistrarEnBD(string objeto, string metodo, string mensaje, string stacktrace)
        {
            string _connString = ConfigurationManager.ConnectionStrings["cadenaConexionBd"].ConnectionString;

            bool Respuesta = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "dbo.RegistrarErrorApp",
                    Connection = new SqlConnection(_connString)
                })
                {
                    cmd.Parameters.AddWithValue("Objeto", objeto);
                    cmd.Parameters.AddWithValue("Metodo", metodo);
                    cmd.Parameters.AddWithValue("Mensaje", mensaje);
                    cmd.Parameters.AddWithValue("StackTrace", stacktrace);

                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    Respuesta = true;
                }
            }
            catch (Exception ex)
            {
                Respuesta = false;
                RegistrarFile(this, ex);
            }

            return Respuesta;
        }

        public static void RegistrarFile(object obj, Exception ex)
        {
            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            string hora = System.DateTime.Now.ToString("HH:mm:ss");
            string path = HttpContext.Current.Request.MapPath("~/log/" + fecha + ".txt");

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + hora);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
            sw.WriteLine("Detalle error: \n" + ex.StackTrace);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }
    }
}