using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IEstadoTipoServer
    {
        Task<IEnumerable<EstadoTipoDtoOut>> GetEstadoTipos();
        Task<EstadoTipo?> GetById(int id);
        Task<EstadoTipoDtoOut?> GetEstadoTipoDtoById(int id);
        Task<EstadoTipo> Create(EstadoTipoDtoIn estadoTipo);
        Task Update(int id, EstadoTipoDtoIn estadoTipo);
        Task Delete(int id);
    }
}