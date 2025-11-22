using Core.Request;
using Core.Response;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly comunidadsolidariaContext _context;
        
        public NotificacionService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NotificacionDtoOut>> GetNotificacions()
        {
            return await _context.Notificacion.Select(m => new NotificacionDtoOut
            {
                Id = m.Id,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                ChatIdChat = m.ChatIdChat,
                Titulo = m.Titulo,
                Descripcion = m.Descripcion,
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombrePerfilReceptorIdPerfilReceptor = m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial

            }).ToArrayAsync();
        }


        public async Task<IEnumerable<NotificacionDtoOut>> GetNotificacionsByReceptor(string nombre)
        {
            return await _context.Notificacion
                .Where(m => m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial == nombre)
                .Select(m => new NotificacionDtoOut
            {
                Id = m.Id,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                ChatIdChat = m.ChatIdChat,
                Titulo = m.Titulo,
                Descripcion = m.Descripcion,
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombrePerfilReceptorIdPerfilReceptor = m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial

            }).ToArrayAsync();
        }

        public async Task<Notificacion?> GetById(int id)
        {
            return await _context.Notificacion.FindAsync(id);
        }

        public async Task<NotificacionDtoOut?> GetNotificacionDtoById(int id)
        {
            return await _context.Notificacion.Where(m => m.Id == id).Select(m => new NotificacionDtoOut
            {
                Id = m.Id,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                ChatIdChat = m.ChatIdChat,
                Titulo = m.Titulo,
                Descripcion = m.Descripcion,
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombrePerfilReceptorIdPerfilReceptor = m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial

            }).SingleOrDefaultAsync();
        }

        public async Task<Notificacion> Create(NotificacionDtoIn notificacion)
        {
            var newNotificacion = new Notificacion();

            newNotificacion.ChatIdChat = notificacion.ChatIdChat;
            newNotificacion.PerfilIdPerfil = notificacion.PerfilIdPerfil;
            newNotificacion.Titulo = notificacion.Titulo;
            newNotificacion.Descripcion = notificacion.Descripcion;
            newNotificacion.PublicacionIdPublicacion = notificacion.PublicacionIdPublicacion;
            newNotificacion.PerfilReceptorIdPerfilReceptor = notificacion.PerfilReceptorIdPerfilReceptor;
            

            _context.Notificacion.Add(newNotificacion);
            await _context.SaveChangesAsync();
            return newNotificacion;
        }

        public async Task Update(int id, NotificacionDtoIn notificacion)
        {
            var existNotificacion = await GetById(id);
            if (existNotificacion != null)
            {
                existNotificacion.ChatIdChat = notificacion.ChatIdChat;
                existNotificacion.PerfilIdPerfil = notificacion.PerfilIdPerfil;
                existNotificacion.Titulo = notificacion.Titulo;
                existNotificacion.Descripcion = notificacion.Descripcion;
                existNotificacion.PublicacionIdPublicacion = notificacion.PublicacionIdPublicacion;
                existNotificacion.PerfilReceptorIdPerfilReceptor = notificacion.PerfilReceptorIdPerfilReceptor;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Notificacion.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
