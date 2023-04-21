using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System;

public class TCPtest : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    bool running;
    public string Mes;
    private string cleardincoming;
    private string cleardincoming2;
    private string Rx;
    
    void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }
    
    void Update()
    {
        if (Mes.Contains("O1G") && Mes.Contains("&"))
        {
            cleardincoming = Mes.Substring(3);                  //delete the message start delimiter
            cleardincoming2 = cleardincoming.Substring(0, cleardincoming.IndexOf("&")-1); //deleting the delimiter and the last @ separator after the last vehicles paramaters
            Rx = cleardincoming2;
        }

    }
    public string RxMsg() //public message, get from Main script
    {
        return Rx;
    }

    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }
    
    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string
        Mes = dataReceived;
        //Debug.Log(dataReceived);
        //---Sending Data to Host----
        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(Choice()); //Converting string to byte data
        nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
    }

    private string Choice()
    {
        string[] charArray = { "one", "two", "three", "four", "five" };
        System.Random random = new System.Random();
        // 随机选择字符数组中的一个元素
        int index = random.Next(0, charArray.Length); // 随机生成一个在 [0, charArray.Length) 区间内的整数
        string selectedChar = charArray[index];
        return selectedChar;
    }
}
