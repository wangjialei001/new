using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppSocket
{
    public class WebSocketTest2
    {
        public const int BufferSize = 4096;
        public string basestringjson = string.Empty;
        WebSocket socket;
        WebSocketTest2(WebSocket socket)
        {
            this.socket = socket;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static async Task Acceptor(HttpContext httpContext,Func<Task> n)
        {
            if (!httpContext.WebSockets.IsWebSocketRequest)
                return;
            var socket = await httpContext.WebSockets.AcceptWebSocketAsync();
            var result = await RecvAsync(socket,CancellationToken.None);
        }
        /// <summary>
        /// 接受客户端数据
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<string> RecvAsync(WebSocket webSocket,CancellationToken cancellationToken)
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);
            while (webSocket.State == WebSocketState.Open)
            {
                var incoming = await webSocket.ReceiveAsync(seg, CancellationToken.None);
                string receivemsg = Encoding.UTF8.GetString(buffer, 0, incoming.Count);
                Console.WriteLine("收到"+ receivemsg + "消息");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(2000);
                    var r = new { now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), count = i,msg= receivemsg };
                    string msg = JsonConvert.SerializeObject(r);
                    await SendAsync(msg, webSocket);
                }
                break;
            }
            return "";
        }
        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public static async Task SendAsync(string msg,WebSocket webSocket)
        {
            CancellationToken cancellation = default(CancellationToken);
            var buf = Encoding.UTF8.GetBytes(msg);
            var segment = new ArraySegment<byte>(buf);
            Console.WriteLine("发送消息");
            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellation);
        }

        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(WebSocketTest2.Acceptor);
        }
    }
}
