using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class LocalidadDtoOut
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? CodigoPostal { get; set; }

        public string? NombreProvinciaIdProvincia { get; set; }
    }
}
