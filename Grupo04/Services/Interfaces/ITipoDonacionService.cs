using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface ITipoDonacionService
    {
        Task<IEnumerable<TipoDonacionDtoOut>> GetTipoDonacions();
        Task<TipoDonacion?> GetById(int id);
        Task<TipoDonacionDtoOut?> GetTipoDonacionDtoById(int id);
        Task<TipoDonacion> Create(TipoDonacionDtoIn tipoDonacion);
        Task Update(int id, TipoDonacionDtoIn tipoDonacion);
        Task Delete(int id);

    }
}