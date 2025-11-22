using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class NotificacionDtoOut
    {
        public int Id { get; set; }

        public string? NombrePerfilIdPerfil { get; set; }

        public int ChatIdChat { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public string? NombrePublicacionIdPublicacion { get; set; }

        public string? NombrePerfilReceptorIdPerfilReceptor { get; set; }
    }
}
