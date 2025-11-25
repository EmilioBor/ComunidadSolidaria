using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class EnvioDtoIn
    {
        public int Id { get; set; }

        public string? DireccionEmisor { get; set; }

        public int PerfilEmisorIdPerfilEmisor { get; set; }

        public int PerfilReceptorIdPerfilReceptor { get; set; }

        public int LocalidadEmisorIdLocalidadEmisor { get; set; }

        public int LocalidadReceptorIdLocalidadReceptor { get; set; }

        public string? DireccionEreceptor { get; set; }

        public int DonacionIddonacion { get; set; }

        public string? Aclaracion { get; set; }
    }
}
