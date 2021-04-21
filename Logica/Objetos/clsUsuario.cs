using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webFacturas.Logica.Objetos
{
    /// <summary>
    /// Clase con los datos del usuario del sistema
    /// </summary>
    public class clsUsuario
    {
        public clsUsuario()
        {
            this.idUsuario = 0;
            this.Nombres = string.Empty;
            this.Apellidos = string.Empty;
            this.Domicilio = string.Empty;
            this.Telefono = string.Empty;
            this.Dpi = string.Empty;
            this.Nit = string.Empty;
            this.PlacaAuto = string.Empty;
            this.IdRol = 0;
            this.Contrasenia = string.Empty;
        }
        public int idUsuario { get; set; }
        public string codigoEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Dpi { get; set; }
        public string Nit { get; set; }
        public string PlacaAuto { get; set; }

        public int IdRol { get; set; }
        public string Contrasenia { get; set; }
    }
}