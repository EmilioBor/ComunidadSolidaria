using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class EnvioDtoIn
    {
        public int Id { get; set; }

        public string? Direccion { get; set; }

        public int LocalidadIdLocalidad { get; set; }

        public DateTime FechaEnvio { get; set; }

        public int EstadoIdEstado { get; set; }
    }
}
