using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Models.Models;

namespace Services.Interfaces
{
    public interface IPerfilService
    {
        Task<IEnumerable<PerfilDtoOut>> GetPerfils();
        Task<Perfil?> GetById(int id);
        Task<PerfilDtoOut?> GetPerfilDtoById(int id);
        Task<Perfil?> GetPerfilIdUsuario(int id);
        Task<Perfil> Create(PerfilDtoIn perfil, IFormFile files);
        Task Update(int id, PerfilDtoIn perfil);
        Task Delete(int id);
    }
}