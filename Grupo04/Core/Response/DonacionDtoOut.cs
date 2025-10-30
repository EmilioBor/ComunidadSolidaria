using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Response
{
    public class DonacionDtoOut
    {
        public int Id { get; set; }

        public DateTime? FechaHora { get; set; }

        public string? NombrePerfilIdPerfil { get; set; }

        public string? NombreDonacionTipoIdDonacionTipo { get; set; }

        public string? Descripcion { get; set; }
    }
}
