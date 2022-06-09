using StudentDataService.WebSocketUtility.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace StudentDataService.WebSocketUtility
{
    public class WebSocketManager : IWebSocketManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public List<WebSocket> GetAllActiveWebSockets()
        {
            return _sockets.Where(x => x.Value.State == WebSocketState.Open).Select(x=>x.Value).ToList();
        }
        public string AddSocket(WebSocket socket)
        {
            string newId = null;
            if (socket != null)
            {
                newId = Guid.NewGuid().ToString();
                if (_sockets.TryAdd(newId, socket))
                {
                    Console.WriteLine($"new web socket added: {newId}, toal sockets open {_sockets.Count}");
                };

            }
            return newId;
        }

        public void RemoveWebSocket(WebSocket socket)
        {
            if(socket != null && _sockets.Any())
            {
                var selectedSocket = _sockets.FirstOrDefault(x => x.Value == socket);
                if (_sockets.TryRemove(selectedSocket))
                {
                    Console.WriteLine($"socket removed, toal sockets open {_sockets.Count}");
                }
            }
        }
    }
}
