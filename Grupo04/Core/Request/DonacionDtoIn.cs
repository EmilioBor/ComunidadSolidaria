using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Request
{
    public class DonacionDtoIn
    {
        public int Id { get; set; }

        public Date? FechaHora { get; set; }

        public int UsuarioIdUsuario { get; set; }

        public int TipoDonacionIdTipoDonacion { get; set; }

        public string? Descripcion { get; set; }
    }
}
