using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class MensajeDtoIn
    {
        public int Id { get; set; }

        public string? Contenido { get; set; }

        public DateTime FechaHora { get; set; }

        public int ChatIdChat { get; set; }

        public int UsuarioIdUsuario { get; set; }
    }
}
