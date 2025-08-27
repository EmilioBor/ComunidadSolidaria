using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class MensajeDtoOut
    {
        public int Id { get; set; }

        public string? Contenido { get; set; }

        public DateTime FechaHora { get; set; }

        public string? NombreChatIdChat { get; set; }

        public string? NombreUsuarioIdUsuario { get; set; }
    }
}
