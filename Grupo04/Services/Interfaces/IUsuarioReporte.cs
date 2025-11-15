using Core.Request;
using Core.Response;
using Models.Models;

namespace Services.Interfaces
{
    public interface IUsuarioReporte
    {
        Task<IEnumerable<UsuarioReporteDtoOut>> GetUsuarioReportes();
        Task<UsuarioReporte?> GetById(int id);
        Task<UsuarioReporteDtoOut?> GetUsuarioReporteDtoById(int id);

        Task<int> CountReportesPorPerfil(int perfilId);
        Task<int> CountReportesPorPublicacion(int publicacionId);
        Task<(UsuarioReporte reporte, int totalReportesPerfil)> CreateConContador(UsuarioReporteDtoIn dto);
        Task Update(int id, UsuarioReporteDtoIn dto);
        Task Delete(int id);
    }
}