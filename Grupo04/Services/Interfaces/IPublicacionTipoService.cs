using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IPublicacionTipoService
    {
        Task<IEnumerable<PublicacionTipoDtoOut>> GetPublicacionTipoAll();
        Task<PublicacionTipo?> GetById(int id);
        Task<PublicacionTipoDtoOut?> GetetalleDonacionTipoDtoById(int id);
        Task<PublicacionTipo> Create(PublicacionTipoDtoIn publicacionTipo);
    }
}