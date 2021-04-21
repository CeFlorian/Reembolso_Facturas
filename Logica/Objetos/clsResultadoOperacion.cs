using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace webFacturas.Logica.Objetos
{
    public class ResultadoBD
    {
        /// <summary>
        /// 0 = Operacion correcta, otro número = Operación fallo
        /// </summary>
        public int codigoRespuesta { get; set; }

        /// <summary>
        /// Contiene los datos que retornó la base de datos si se realizó una consulta.
        /// </summary>
        public DataTable Datos { get; set; }

        public ResultadoBD()
        {
            this.codigoRespuesta = -99;
            this.Datos = new DataTable();
        }
    }

    public class Resultado
    {
        /// <summary>
        /// True = la tarea se realizó correctamente, False = La tarea no se realizó correctamente
        /// </summary>
        public bool Exitoso { get; set; }

        /// <summary>
        /// Codigo para realizar algúna acción determinada en la capa de presentación
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Mensaje que se pude mostrar en la capa de presentación
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Datos retornados por el método utilizado. Castear según el tipo de que se retorne(Ver descripcion del método)
        /// </summary>
        public object Datos { get; set; }

        public Resultado()
        {
            this.Exitoso = false;
            this.Mensaje = "Error no especificado";
            this.Datos = null;
            this.Codigo = -99;
        }
    }
}