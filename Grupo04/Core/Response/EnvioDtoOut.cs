using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class EnvioDtoOut
    {
        public int Id { get; set; }

        public string? DireccionEmisor { get; set; }

        public string? PerfilEmisorIdPerfilEmisor { get; set; }

        public string? PerfilReceptorIdPerfilReceptor { get; set; }

        public string? LocalidadEmisorIdLocalidadEmisor { get; set; }

        public string? LocalidadReceptorIdLocalidadReceptor { get; set; }

        public string? DireccionEreceptor { get; set; }

        public string? DonacionIddonacion { get; set; }

        public string? Aclaracion { get; set; }
    }
}
