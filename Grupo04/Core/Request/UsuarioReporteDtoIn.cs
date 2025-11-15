using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class UsuarioReporteDtoIn
    {
        public int Id { get; set; }

        public string? Motivo { get; set; }

        public string? Descripcion { get; set; }

        public int PublicacionIdPublicacion { get; set; }

        public int PerfilIdPerfil { get; set; }

        public DateTime FechaHora { get; set; }
    }
}
