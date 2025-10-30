using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IDetalleDonacionTipoService
    {
        Task<IEnumerable<DetalleDonacionTipoDtoOut>> GetDetalleDonacionTipoAll();
        Task<DetalleDonacionTipo?> GetById(int id);
        Task<DetalleDonacionTipoDtoOut?> GetetalleDonacionTipoDtoById(int id);
        Task<DetalleDonacionTipo> Create(DetalleDonacionTipoDtoIn detalleDonacionTipo);
    }
}