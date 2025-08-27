using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Request
{
    public class EnvioDtoIn
    {
        public int Id { get; set; }

        public string? Direccion { get; set; }

        public int LocalidadIdLocalidad { get; set; }

        public Date? FechaEnvio { get; set; }

        public int EstadoIdEstado { get; set; }
    }
}
