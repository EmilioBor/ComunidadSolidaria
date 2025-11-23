using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class DonacionDetalleEstadoDtoIn
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public int DonacionIdDonacion { get; set; }

        public int Cantidad { get; set; }


    }
}
