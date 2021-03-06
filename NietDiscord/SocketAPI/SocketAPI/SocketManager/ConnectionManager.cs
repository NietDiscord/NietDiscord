using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;

namespace NietDiscord.SocketManager
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> connections = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return connections.FirstOrDefault(x => x.Key == id).Value;
        }

        public ConcurrentDictionary<string,WebSocket> GetAllConnections()
        {
            return connections;
        }

        public string GetId(WebSocket socket)
        {
            return connections.FirstOrDefault(x => x.Value == socket).Key;
        }

        public async Task RemoveSocketAsync(string id)
        {
            connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket connection closed",
                CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            connections.TryAdd(GetConnectionId(), socket);
        }

        private string GetConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
