using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IPublicacionService
    {
        Task<IEnumerable<PublicacionDtoOut>> GetPublicacions();
        Task<Publicacion?> GetById(int id);
        Task<PublicacionDtoOut?> GetPublicacionDtoById(int id);
        Task<Publicacion> Create(PublicacionDtoIn publicacion);
        Task Update(int id, PublicacionDtoIn publicacion);
        Task Delete(int id);

    }
}