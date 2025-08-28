using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface INovedadService
    {
        Task<IEnumerable<NovedadDtoOut>> GetNovedads();
        Task<Novedad?> GetById(int id);
        Task<NovedadDtoOut?> GetNovedadDtoById(int id);
        Task<Novedad> Create(NovedadDtoIn novedad);
        Task Update(int id, NovedadDtoIn novedad);
        Task Delete(int id);
    }
}