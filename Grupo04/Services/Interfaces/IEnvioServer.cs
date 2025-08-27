using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IEnvioServer
    {
        Task<IEnumerable<EnvioDtoOut>> GetEnvios();
        Task<Envio?> GetById(int id);
        Task<EnvioDtoOut?> GetEnvioDtoById(int id);
        Task<Envio> Create(EnvioDtoIn envio);
        Task Update(int id, EnvioDtoIn envio);
        Task Delete(int id);

    }
}