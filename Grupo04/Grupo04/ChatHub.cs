using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Services.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatId, int perfilId, string message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", perfilId, message);
        }

        public override async Task OnConnectedAsync()
        {
            var chatId = Context.GetHttpContext()!.Request.Query["chatId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId!);
            await base.OnConnectedAsync();
        }
    }
}
