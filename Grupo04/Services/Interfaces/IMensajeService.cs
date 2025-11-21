using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IMensajeService
    {
        Task<IEnumerable<MensajeDtoOut>> GetMensajes();
        Task<Mensaje?> GetById(int id);
        Task<MensajeDtoOut?> GetMensajeDtoById(int id);
        Task<Mensaje> Create(MensajeDtoIn mensaje);
        Task Update(int id, MensajeDtoIn mensaje);
        Task Delete(int id);
        Task<IEnumerable<MensajeDtoOut>> GetMessages(int chatId);
    }
}