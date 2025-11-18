using Microsoft.AspNetCore.SignalR;

namespace Grupo04.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatId, int perfilId, string message)
        {
            await Clients.Group(chatId.ToString())
                .SendAsync("ReceiveMessage", perfilId, message);
        }

        public override async Task OnConnectedAsync()
        {
            var chatId = Context.GetHttpContext()!.Request.Query["chatId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId!);

            await base.OnConnectedAsync();
        }
    }
}
