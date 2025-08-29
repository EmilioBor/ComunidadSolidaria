using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IProvinciaService
    {
        Task<IEnumerable<ProvinciaDtoOut>> GetProvincias();
        Task<Provincia?> GetById(int id);
        Task<ProvinciaDtoOut?> GetProvinciaDtoById(int id);
        Task<Provincia> Create(ProvinciaDtoIn provincia);
        Task Update(int id, ProvinciaDtoIn provincia);
        Task Delete(int id);
    }
}