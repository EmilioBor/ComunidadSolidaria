using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class DonacionDtoIn
    {
        public int Id { get; set; }

        public DateTime? FechaHora { get; set; }

        public int PerfilIdPerfil { get; set; }

        public int DonacionTipoIdDonacionTipo { get; set; }

        public string? Descripcion { get; set; }
    }
}
