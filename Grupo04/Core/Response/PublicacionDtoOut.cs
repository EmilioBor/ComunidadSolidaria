using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class PublicacionDtoOut
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public byte[]? Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? NombreLocalidadIdLocalidad { get; set; }

        public string? NombrePerfilIdPerfil { get; set; }

        public string? NombrePublicacionTipoIdPublicacionTipo { get; set; }

        public string? NombreDonacionIdDonacion { get; set; }
    }
}
