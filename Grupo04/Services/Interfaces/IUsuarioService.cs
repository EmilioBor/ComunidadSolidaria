using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDtoOut>> GetUsuarios();
        Task<Usuario?> GetById(int id);
        Task<UsuarioDtoOut?> GetUsuarioDtoById(int id);
        Task<UsuarioAuthResponse> Login(string email, string password);
        Task<UsuarioAuthResponse> Create(UsuarioDtoIn usuario);
        Task Update(int id, UsuarioDtoIn usuario);
        Task Delete(int id);
    }
}