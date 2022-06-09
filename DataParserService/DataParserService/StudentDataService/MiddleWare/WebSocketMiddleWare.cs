using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StudentDataService.WebSocketUtility.Interface;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentDataService.MiddleWare
{
    public class WebSocketMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IWebSocketManager _socketManager;
        public WebSocketMiddleWare(RequestDelegate next, IWebSocketManager webSocketManger)
        {
            _next = next;
            _socketManager = webSocketManger; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                _socketManager.AddSocket(webSocket);

                await ReceiveMessage(webSocket, (result, buffer) =>
                {
                    if (webSocket.State == WebSocketState.CloseReceived)
                    {
                        //code to remove the socket connection from the token manager class
                        Console.WriteLine("closing the web socket");
                        _socketManager.RemoveWebSocket(webSocket);
                    }
                });
            }
            else
            {
                await _next(context);
            }
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while(socket.State == WebSocketState.Open)
            {
                var results = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine(results.ToString());
                handleMessage(results, buffer);
            }
        }

    }

    public static class ApplicationSettingExtension
    {
        public static IApplicationBuilder UseWebSocketMiddleWare(this IApplicationBuilder application)
        {
            return application.UseMiddleware<WebSocketMiddleWare>();
        }
    }
}
