using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IDonacionTipoService
    {
        Task<IEnumerable<DonacionTipoDtoOut>> GetDonacionTipoAll();
        Task<DonacionTipo?> GetById(int id);

        Task<DonacionTipoDtoOut?> GetDonacionTipoDtoById(int id);

        Task<DonacionTipo> Create(DonacionTipoDtoIn donacionTipo);
    }
}