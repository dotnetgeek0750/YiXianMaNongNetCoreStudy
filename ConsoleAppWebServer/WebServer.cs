using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleAppWebServer
{

    public class WebServer
    {
        //声明一个IPv4、流、TCP协议的Socket
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        public static void Start()
        {
            //socket绑定任意IP、端口8012
            socket.Bind(new IPEndPoint(IPAddress.Any, 8012));
            socket.Listen(100);
            //接受客户端的socket请求
            socket.BeginAccept(OnAccept, socket);
            Console.WriteLine("web服务器开启，监听端口为8012");
        }

        //接受请求
        public static void OnAccept(IAsyncResult async)
        {
            //客户端socket
            var serverSocket = async.AsyncState as Socket;
            //客户端的socket
            var clientSocket = serverSocket.EndAccept(async);
            //进行下一步监听
            serverSocket.BeginAccept(OnAccept, serverSocket);
            //获取socket的内容
            var bytes = new byte[10000];
            var len = clientSocket.Receive(bytes);
            //将bytes[]转成string
            //request就是客户端提交过来的HTTP报文
            var request = Encoding.UTF8.GetString(bytes, 0, len);

            var response = string.Empty;
            if (!string.IsNullOrWhiteSpace(request) && !request.Contains("favicon.ico"))
            {
                var filePath = request.Split("\r\n")[0].Split(" ")[1].TrimStart('/');
                response = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
            }
            //按照HTTP的响应报文返回
            var responseHeader = string.Format(@"HTTP/1.1 200 OK
Date: Wed, 19 Dec 2018 11:20:55 GMT
Content - Type: text / html; charset = utf - 8
Connection: keep - alive
Vary: Accept - Encoding
Cache - Control: public, max-age=21
Expires: Wed, 19 Dec 2018 11:21:17 GMT
Last-Modified: Wed, 19 Dec 2018 11:20:47 GMT
X-UA-Compatible: IE=10
X-Frame-Options: SAMEORIGIN
Content-Length: {0}", Encoding.UTF8.GetByteCount(response));

            clientSocket.Send(Encoding.UTF8.GetBytes(responseHeader));
            clientSocket.Send(Encoding.UTF8.GetBytes(response));
            clientSocket.Close();
        }
    }
}
