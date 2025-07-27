using DataProvider.DatabaseContext;
using DataProvider.Interfaces.Chat;
using DataProvider.Interfaces.Core;
using DataProvider.Models.Chat;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace MansiaWebApi.Hubs
{
    public class ChatHub : Hub
    {
        //private readonly ApplicationDbContext dbContext;
        private readonly IChatHubConnectionRepository chatHubConnectionRepository;
        private readonly IUnitOfWork unitOfWork;
        public ChatHub(IChatHubConnectionRepository chatHubConnectionRepository, IUnitOfWork unitOfWork)
        {
            //this.dbContext = dbContext;          
            this.chatHubConnectionRepository = chatHubConnectionRepository;     
            this.unitOfWork = unitOfWork;   
        }

        public async Task JoinChat(UserConnection conn) {
            await Clients.All
                .SendAsync("ReceiveMessage", "Admin", $"{conn.UserName} has joined chat.");
        }

        public override async Task OnConnectedAsync()
        {
            //await SaveUserConnection("", "");
            Clients.Caller.SendAsync("OnConnected");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = await chatHubConnectionRepository.GetByConnectionId(Context.ConnectionId);
            if(hubConnection != null)
            {
                await chatHubConnectionRepository.RemoveUserConnection(hubConnection);
                await unitOfWork.CommitAsync(); // Await SaveChangesAsync
            }

            await base.OnDisconnectedAsync(exception); // Ensure base method is awaited
        }

        public async Task SaveUserConnection(string username, Guid userid)
        {
            var connectionId = Context.ConnectionId;
            ChatHubConnection hubConnection = new ChatHubConnection
            {
                ConnectionId = connectionId,
                UserName = username,
                UserId = userid
            };
            chatHubConnectionRepository.SaveUserConnection(hubConnection);

            //dbContext.ChatHubConnections.Add(hubConnection);
            try
            {
               await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        }

        public async Task SendMessageToAll(string msg)
        {
            await Clients.All.SendAsync("ReceivedMessage", msg);
        }

        public async Task SendMessageToSpecificUser(string Message)
        {
            var connectionIds = (await chatHubConnectionRepository.GetAllAsync()).Select(x=> x.ConnectionId);
            foreach(var connId in connectionIds)
            {
                await Clients.Client(connId).SendAsync("ReceiveMessage", Message);
            }
        }
    }
}
