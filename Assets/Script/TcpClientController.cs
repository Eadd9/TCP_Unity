using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TcpClientController : MonoBehaviour
{
    private const string SERVER_IP = "127.0.0.1"; // 服务器 IP 地址
    private const int SERVER_PORT = 5000; // 服务器端口号

    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;
    private String mes;

    void Start()
    {
        // 创建TCP客户端
        client = new TcpClient(SERVER_IP, SERVER_PORT);
        stream = client.GetStream();
        
    }

    private void Update()
    {
        
        // 按下回车键发送消息
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage("Hello, get");
        }
        
        // 启动接收消息线程
        receiveThread = new Thread(new ThreadStart(ReceiveMessage));
        receiveThread.Start();
    }

    private void SendMessage(string message)
    {
        // 向服务器发送消息
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private void ReceiveMessage()
    {
        byte[] data = new byte[1024];

        while (true)
        {
            int length = stream.Read(data, 0, data.Length);
            if (length == 0)
            {
                // 与服务器断开连接
                Debug.Log("与服务器断开连接");
                break;
            }

            string message = Encoding.UTF8.GetString(data, 0, length);
            if (message.Contains("O1G") && message.Contains("&"))
            {
                String mes = message;
                Debug.Log("收到消息：" + mes);
            }
            
        }
    }

    private void OnDestroy()
    {
        // 关闭连接
        stream.Close();
        client.Close();

        // 停止接收消息线程
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
    }
}