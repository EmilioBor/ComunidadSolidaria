using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IDonacionEstadoService
    {
        Task<IEnumerable<DonacionEstadoDtoOut>> GetDonacionEstadoAll();
        Task<DonacionEstado?> GetById(int id);
        Task<DonacionEstadoDtoOut?> GetetalleDonacionTipoDtoById(int id);
        Task<DonacionEstado> Create(DonacionEstadoDtoIn donacionEstado);
        Task<DonacionEstadoDtoOut?> GetDetalleDonacionTipoDtoByNombre(string estado);
    }
}