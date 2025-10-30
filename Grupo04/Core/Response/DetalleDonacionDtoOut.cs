using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class DetalleDonacionDtoOut
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public string? NombreEnvioIdEnvio { get; set; }

        public string? NombreDonacionIdDonacion { get; set; }

        public string? NombreDetalleDonacionTipoIdDetalleDonacinoTipo {  get; set; }
    }
}
