using Core.Request;
using Core.Response;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDetalleDonacionService
    {
        Task<IEnumerable<DetalleDonacionDtoOut>> GetDetalleDonacions();
        Task<DetalleDonacion?> GetById(int id);
        Task<DetalleDonacionDtoOut?> GetDetalleDonacionDtoById(int id);
        Task<DetalleDonacion> Create(DetalleDonacionDtoIn detalleDonacion);
        Task Update(int id, DetalleDonacionDtoIn detalleDonacion);
        Task Delete(int id);
    }
}
