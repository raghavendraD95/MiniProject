using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace StudentDataService.WebSocketUtility.Interface
{
    public interface IWebSocketManager
    {
        string AddSocket(WebSocket socket);
        void RemoveWebSocket(WebSocket socket);
        List<WebSocket> GetAllActiveWebSockets();
    }
}
