using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatServer _service;
        public ChatController(IChatServer service)
        {
            _service = service;
        }

        [HttpGet("api/v1/chats")]
        public async Task<IEnumerable<ChatDtoOut>> Chats()
        {
            return await _service.GetChats();
        }

        [HttpGet("api/v1/chat/id/{id}")]
        public async Task<ActionResult<ChatDtoOut>> GetChatById(int id)
        {
            var chat = await _service.GetChatDtoById(id);
            if(chat is null)
            {
                return NotFound(id);
            }
            else
            {
                return chat;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/chat")]
        public async Task<IActionResult> Create(ChatDtoIn chatDtoIn)
        {
            var newChat = await _service.Create(chatDtoIn);

            return CreatedAtAction(nameof(GetChatById), new {id = newChat.Id}, newChat);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, ChatDtoIn chatDtoIn)
        {
            if( id != chatDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({chatDtoIn.Id}) del cuerpo de la solicitud" });

            var chatToUpdate = await _service.GetChatDtoById(id);

            if(chatToUpdate is not null)
            {
                await _service.Update(id, chatDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }
            
        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/chat/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetChatDtoById(id);

            if (toDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
