using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class UsuarioAuthResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Rol {  get; set; }
        public bool EsCorrecto { get; set; }
    }
}
