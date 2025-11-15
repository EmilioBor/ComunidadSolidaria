using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class UsuarioReporteDtoOut
    {
        public int Id { get; set; }

        public string? Motivo { get; set; }

        public string? Descripcion { get; set; }

        public string? NombrePublicacionIdPublicacion { get; set; }

        public string? NombrePerfilIdPerfil { get; set; }

        public DateTime FechaHora { get; set; }
    }
}
