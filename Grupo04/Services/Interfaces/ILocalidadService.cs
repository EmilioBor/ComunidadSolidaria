using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface ILocalidadService
    {
        Task<IEnumerable<LocalidadDtoOut>> GetLocalidads();
        Task<Localidad?> GetById(int id);
        Task<LocalidadDtoOut?> GetLocalidadDtoById(int id);
        Task<Localidad> Create(LocalidadDtoIn localidad);
        Task Update(int id, LocalidadDtoIn localidad);
        Task Delete(int id);
    }
}