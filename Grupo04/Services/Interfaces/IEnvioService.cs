using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IEnvioService
    {
        Task<Envio> Create(EnvioDtoIn Envio);
        Task<EnvioDtoOut?> GetEnvioDtoById(int id);
        Task<IEnumerable<EnvioDtoOut>> GetEnvioAll();
    }
}