using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IDonacionService
    {
        Task<IEnumerable<DonacionDtoOut>> GetDonacions();
        Task<Donacion?> GetById(int id);
        Task<DonacionDtoOut?> GetDonacionDtoById(int id);
        Task<Donacion> Create(DonacionDtoIn donacion);
        Task Update(int id, DonacionDtoIn donacion);
        Task Delete(int id);
    }
}