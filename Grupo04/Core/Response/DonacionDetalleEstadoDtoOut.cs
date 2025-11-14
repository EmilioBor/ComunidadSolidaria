using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class DonacionDetalleEstadoDtoOut
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public string? NombreDonacionIdDonacion { get; set; }

        public int Cantidad { get; set; }

        public string? NombreDonacionEstadoIdDonacionEstado { get; set; }
    }
}
