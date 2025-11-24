using Core.Request;
using Core.Response;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDonacionDetalleEstadoService
    {
        Task<IEnumerable<DonacionDetalleEstadoDtoOut>> GetDonacionDetalleEstados();
        Task<DonacionDetalleEstado?> GetById(int id);
        Task<DonacionDetalleEstadoDtoOut?> GetDonacionDetalleEstadoDtoById(int id);
        Task<DonacionDetalleEstado> Create(DonacionDetalleEstadoDtoIn donacionDetalleEstado);
        Task Update(int id, DonacionDetalleEstadoDtoIn donacionDetalleEstado);
        Task<DonacionDetalleEstadoDtoOut?> GetDonacionDetalleEstadoUltimo(string descripcion);
        
        Task Delete(int id);
    }
}
