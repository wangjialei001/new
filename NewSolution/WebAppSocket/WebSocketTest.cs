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
    public class WebSocketTest
    {
        WebSocket socket;
        WebSocketTest(WebSocket socket)
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
            string oldRequestParam = "";
            WebSocketReceiveResult result;
            do
            {
                var ms = new MemoryStream();
                var buffer = new ArraySegment<byte>(new byte[1024 * 8]);
                result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                if (result.MessageType.ToString() == "Close")
                {
                    break;
                }
                ms.Write(buffer.Array, buffer.Offset, result.Count - buffer.Offset);
                ms.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(ms);
                var s = reader.ReadToEnd();
                reader.Dispose();
                ms.Dispose();
                if (!string.IsNullOrEmpty(s))
                {
                    await SendAsync(s, webSocket);
                }
                oldRequestParam = s;
            } while (result.EndOfMessage);
            return "";
        }
        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public static async Task SendAsync(string msg, WebSocket webSocket)
        {
            CancellationToken cancellation = default(CancellationToken);
            var r = new { now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            var buf = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(r));
            var segment = new ArraySegment<byte>(buf);
            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellation);
        }

        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(WebSocketTest.Acceptor);
        }
    }
}
