using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class PerfilDtoOut
    {
        public int Id { get; set; }

        public int CuitCuil { get; set; }

        public string? RazonSocial { get; set; }

        public string? Descripcion { get; set; }

        public int Cbu { get; set; }

        public string? Alias { get; set; }

        public string? NombreUsuarioIdUsuario { get; set; }

        public string? NombreLocalidadIdLocalidad { get; set; }

        public byte[]? Imagen { get; set; }
    }
}
