using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PersonalNotesAPI.Hubs.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalNotesAPI.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();
        public IUserIdProvider _userIdProvider;
        public ChatHub(IUserIdProvider userIdProvider)
        {
            _userIdProvider = userIdProvider;
        }
        public void SendChatMessage(string who, string message)
        {
            string username = Context.User.Identity.Name;            
            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).SendAsync("ReceiveMessage", username, message);
            }
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                _connections.Add(name, conn);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
