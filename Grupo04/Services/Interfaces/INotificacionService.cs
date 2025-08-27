using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface INotificacionService
    {
        Task<IEnumerable<NotificacionDtoOut>> GetNotificacions();
        Task<Notificacion?> GetById(int id);
        Task<NotificacionDtoOut?> GetNotificacionDtoById(int id);
        Task<Notificacion> Create(NotificacionDtoIn notificacion);
        Task Update(int id, NotificacionDtoIn notificacion);
        Task Delete(int id);
    }
}