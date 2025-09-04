using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Models.Models;

namespace Services.Interfaces
{
    public interface INovedadService
    {
        Task<IEnumerable<NovedadDtoOut>> GetNovedads();
        Task<Novedad?> GetById(int id);
        Task<NovedadDtoOut?> GetNovedadDtoById(int id);
        Task<Novedad> Create(NovedadDtoIn novedad, IFormFile files);
        Task Update(int id, NovedadDtoIn novedad);
        Task Delete(int id);
    }
}