using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class PerfilDtoIn
    {
        public int Id { get; set; }

        public long CuitCuil { get; set; }

        public string? RazonSocial { get; set; }

        public string? Descripcion { get; set; }

        public long Cbu { get; set; }

        public string? Alias { get; set; }

        public int UsuarioIdUsuario { get; set; }

        public int LocalidadIdLocalidad { get; set; }

        public byte[]? Imagen { get; set; }
    }
}
