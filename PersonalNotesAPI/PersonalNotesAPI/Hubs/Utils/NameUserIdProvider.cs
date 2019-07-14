using Microsoft.AspNetCore.SignalR;

namespace PersonalNotesAPI.Hubs.Utils
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
