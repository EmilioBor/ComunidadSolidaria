using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class NotificacionDtoIn
    {
        public int Id { get; set; }

        public int PerfilIdPerfil { get; set; }

        public int ChatIdChat { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public int NovedadIdNovedad { get; set; }
    }
}
