using DataProvider.DatabaseContext;
using DataProvider.Models.Chat;
using Microsoft.AspNetCore.SignalR;

namespace MansiaWebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext dbContext;
        public ChatHub(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task JoinChat(UserConnection conn) {
            await Clients.All
                .SendAsync("ReceiveMessage", "Admin", $"{conn.UserName} has joined chat.");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        }

        public async Task SendMessageToAll(string msg)
        {
            await Clients.All.SendAsync("ReceivedMessage", msg);
        }

        public async Task SendMessageToSpecificUser()
        {

        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }
        public async Task SaveUserConnection(string username, string userid)
        {
            var connectionId = Context.ConnectionId;
            ChatHubConnection hubConnection = new ChatHubConnection
            {
                ConnectionId = connectionId,
                UserName = username,
                UserId = userid
            };

            dbContext.ChatHubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext.ChatHubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                dbContext.ChatHubConnections.Remove(hubConnection);
                await dbContext.SaveChangesAsync(); // Await SaveChangesAsync
            }

            await base.OnDisconnectedAsync(exception); // Ensure base method is awaited
        }
    }
}
