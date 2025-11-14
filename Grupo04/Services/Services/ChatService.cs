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
    public class ChatService : IChatServer
    {
        private readonly comunidadsolidariaContext _context;

        public ChatService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ChatDtoOut>> GetChats()
        {
            return await _context.Chat.Select(m => new ChatDtoOut
            {
                Id = m.Id,
                NombrePerfilidPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombreReceptorIdReceptor = m.ReceptorIdReceptorNavigation.RazonSocial,

            }).ToArrayAsync();
        }
        public async Task<Chat?> GetById (int id)
        {
            return await _context.Chat.FindAsync(id);
        }

        public async Task<ChatDtoOut?> GetChatDtoById(int id)
        {
            return await _context.Chat.Where(m => m.Id == id).Select(m => new ChatDtoOut
            {
                Id = m.Id,
                NombrePerfilidPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombreReceptorIdReceptor = m.ReceptorIdReceptorNavigation.RazonSocial,

            }).SingleOrDefaultAsync();
        }

        public async Task<Chat> Create(ChatDtoIn chat)
        {
            if (chat.PerfilIdPerfil == chat.ReceptorIdReceptor)
            {
                throw new ArgumentException("El perfil y el receptor no pueden tener el mismo ID.");
            }

            var existingChat = await GetExistingChatAsync(chat.PerfilIdPerfil, chat.ReceptorIdReceptor);
            if (existingChat != null)
                return existingChat;

            var newChat = new Chat();

            newChat.PerfilIdPerfil = chat.PerfilIdPerfil;
            newChat.PublicacionIdPublicacion = chat.PublicacionIdPublicacion;
            newChat.ReceptorIdReceptor = chat.ReceptorIdReceptor;

            _context.Chat.Add(newChat);
            await _context.SaveChangesAsync();
            return newChat;
        }
        //Duplicacion de mensaje
        public async Task<Chat?> GetExistingChatAsync(int perfilId, int receptorId)
        {
            return await _context.Chat
                .FirstOrDefaultAsync(c =>
                    (c.PerfilIdPerfil == perfilId && c.ReceptorIdReceptor == receptorId) ||
                    (c.PerfilIdPerfil == receptorId && c.ReceptorIdReceptor == perfilId)
                );
        }

        public async Task Update(int id, ChatDtoIn chat)
        {
            var existChat = await GetById(id);
            if (existChat != null)
            {
                existChat.PerfilIdPerfil = chat.PerfilIdPerfil;
                existChat.PublicacionIdPublicacion = chat.PublicacionIdPublicacion;
                existChat.ReceptorIdReceptor = chat.ReceptorIdReceptor;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if(toDelete != null)
            {
                _context.Chat.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
