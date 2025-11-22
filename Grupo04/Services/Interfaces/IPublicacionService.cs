using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Models.Models;

namespace Services.Interfaces
{
    public interface IPublicacionService
    {
        Task<IEnumerable<PublicacionDtoOut>> GetPublicacions();
        Task<Publicacion?> GetById(int id);
        Task<PublicacionDtoOut?> GetPublicacionDtoById(int id);
        Task<Publicacion> Create(PublicacionDtoIn publicacion, IFormFile files);
        Task Update(int id, PublicacionDtoIn publicacion);
        Task Delete(int id);


        Task<PublicacionDtoOut?> GetPublicacionDtoByTitulo(string titulo);
        Task<IEnumerable<PublicacionDtoOut>> GetPublicacionDtoByPerfil(string name);
        Task<IEnumerable<PublicacionDtoOut>> GetPublicacionDtoByTipoPubli(string name);
    }
}