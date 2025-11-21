using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IChatServer
    {
        Task<IEnumerable<ChatDtoOut>> GetChats();
        Task<Chat?> GetById(int id);
        Task<ChatDtoOut?> GetChatDtoById(int id);
        Task<IEnumerable<ChatDtoOut?>> GetChatDtoByPerfil(string nombre);
        Task<Chat> Create(ChatDtoIn chat);
        Task Update(int id, ChatDtoIn chat);
        Task Delete(int id);
    }
}