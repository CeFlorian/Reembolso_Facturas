using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webFacturas.Logica.Objetos
{
    public class SolicitudFactura
    {
        public int IdSolicitud { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }

        public int IdProyecto { get; set; }
        public string LugarEmitido { get; set; }
        public int Cantidad { get; set; }
        public double MontoTotal { get; set; }
        public string Descripcion { get; set; }
        public int IdConcepto { get; set; }
        public int IdEstado { get; set; }
        public int IdUsuario { get; set; }
    }
}