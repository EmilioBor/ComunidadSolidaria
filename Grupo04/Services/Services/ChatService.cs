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
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
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
                NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
            }).SingleOrDefaultAsync();
        }

        public async Task<Chat> Create(ChatDtoIn chat)
        {
            var newChat = new Chat();

            newChat.PublicacionIdPublicacion = newChat.PublicacionIdPublicacion;
            newChat.UsuarioIdUsuarioNavigation = newChat.UsuarioIdUsuarioNavigation;

            _context.Chat.Add(newChat);
            await _context.SaveChangesAsync();
            return newChat;
        }

        public async Task Update(int id, ChatDtoIn chat)
        {
            var existChat = await GetById(id);
            if (existChat != null)
            {
                existChat.PublicacionIdPublicacion = chat.IdPublicacion;
                existChat.UsuarioIdUsuario = chat.UsuarioIdUsuario;
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
