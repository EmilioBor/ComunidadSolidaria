using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Response
{
    public class EnvioDtoOut
    {
        public int Id { get; set; }

        public string? Direccion { get; set; }

        public string? NombreLocalidadIdLocalidad { get; set; }

        public Date? FechaEnvio { get; set; }

        public string? NombreEstadoIdEstado { get; set; }
    }
}
