using Core.Request;
using Core.Response;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;

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
            return await _context.Chat
                .Include(c => c.PerfilIdPerfilNavigation)
                .Include(c => c.ReceptorIdReceptorNavigation)
                .Include(c => c.PublicacionIdPublicacionNavigation)
                .Select(m => new ChatDtoOut
                {
                    Id = m.Id,
                    NombrePerfilidPerfil = m.PerfilIdPerfilNavigation!.RazonSocial,
                    NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation!.Titulo,
                    NombreReceptorIdReceptor = m.ReceptorIdReceptorNavigation!.RazonSocial
                }).ToListAsync();
        }

        public async Task<Chat?> GetById(int id)
        {
            return await _context.Chat
                .Include(c => c.PerfilIdPerfilNavigation)
                .Include(c => c.ReceptorIdReceptorNavigation)
                .Include(c => c.PublicacionIdPublicacionNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ChatDtoOut?> GetChatDtoById(int id)
        {
            return await _context.Chat
                .Where(c => c.Id == id)
                .Include(c => c.PerfilIdPerfilNavigation)
                .Include(c => c.ReceptorIdReceptorNavigation)
                .Include(c => c.PublicacionIdPublicacionNavigation)
                .Select(m => new ChatDtoOut
                {
                    Id = m.Id,
                    NombrePerfilidPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                    NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                    NombreReceptorIdReceptor = m.ReceptorIdReceptorNavigation.RazonSocial
                }).SingleOrDefaultAsync();
        }



        public async Task<IEnumerable<ChatDtoOut?>> GetChatDtoByPerfil(string nombre)
        {
            return await _context.Chat
                .Where(c =>
                    c.PerfilIdPerfilNavigation.RazonSocial == nombre ||
                    c.ReceptorIdReceptorNavigation.RazonSocial == nombre
                )
                .Include(c => c.PerfilIdPerfilNavigation)
                .Include(c => c.ReceptorIdReceptorNavigation)
                .Include(c => c.PublicacionIdPublicacionNavigation)
                .Select(m => new ChatDtoOut
                {
                    Id = m.Id,
                    NombrePerfilidPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                    NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo,
                    NombreReceptorIdReceptor = m.ReceptorIdReceptorNavigation.RazonSocial
                })
                .ToListAsync();
        }


        public async Task<Chat> Create(ChatDtoIn chat)
        {
            if (chat.PerfilIdPerfil == chat.ReceptorIdReceptor)
                throw new ArgumentException("El perfil y el receptor no pueden tener el mismo ID.");

            var existingChat = await GetExistingChatAsync(chat.PerfilIdPerfil, chat.ReceptorIdReceptor);
            if (existingChat != null)
                return existingChat;

            var newChat = new Chat
            {
                PerfilIdPerfil = chat.PerfilIdPerfil,
                PublicacionIdPublicacion = chat.PublicacionIdPublicacion,
                ReceptorIdReceptor = chat.ReceptorIdReceptor
            };

            _context.Chat.Add(newChat);
            await _context.SaveChangesAsync();

            return newChat;
        }

        public async Task<Chat?> GetExistingChatAsync(int perfilId, int receptorId)
        {
            return await _context.Chat.FirstOrDefaultAsync(c =>
                (c.PerfilIdPerfil == perfilId && c.ReceptorIdReceptor == receptorId) ||
                (c.PerfilIdPerfil == receptorId && c.ReceptorIdReceptor == perfilId)
            );
        }

        public async Task Update(int id, ChatDtoIn chat)
        {
            var existChat = await GetById(id);

            if (existChat == null)
                throw new KeyNotFoundException("Chat no encontrado");

            existChat.PerfilIdPerfil = chat.PerfilIdPerfil;
            existChat.PublicacionIdPublicacion = chat.PublicacionIdPublicacion;
            existChat.ReceptorIdReceptor = chat.ReceptorIdReceptor;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);

            if (toDelete == null)
                throw new KeyNotFoundException("Chat no encontrado");

            _context.Chat.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }
}
