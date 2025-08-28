using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class NovedadDtoOut
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string? NombrePerfilIdPerfil { get; set; }

        public byte[]? Imagen { get; set; }
    }
}
