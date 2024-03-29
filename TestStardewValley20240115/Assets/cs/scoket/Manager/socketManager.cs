using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;

public class socketManager : MonoBehaviour
{
    //与服务端的接套字
    Socket socket;
    //服务端的ip与端口
    public InputField host;
    public InputField port;
    //要发送的消息
    public InputField body;
    //显示内容
    public Text recvText;
    public Text clienText;
    //接收缓冲区
    const int BUFFER_SIZE = 1024;
    byte[] readBuff = new byte[BUFFER_SIZE];
    //显示的内容字符串
    public string recvBody = string.Empty;

    //只有主线程才能修改UI组件的属性
    //因次将内容保存在字符串,然后修改ui文本
    private void Update()
    {
        //显示消息
        recvText.text = recvBody;
    }

    //连接服务器
    public void Connetion()
    {
        //连接时清理原来的显示
        recvText.text = "";

        //Socket 创建客户端接套字
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //服务端ip与端口
        string host = this.host.text;
        int port = int.Parse(this.port.text);

        //Connect 连接服务端
        socket.Connect(host, port);
        clienText.text = "客户端地址:" + socket.LocalEndPoint.ToString();

        Send("GetMsg");

        //Receive 接收服务端消息 count 消息长度
        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
    }

    //接收回调
    public void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            //count数据大小
            int count = socket.EndReceive(ar);
            //处理接收的数据
            string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);

            string[] cs = str.Split(' ');

            if(cs[0] == "GetMsg")
            {
                for (int i = 1; i < cs.Length; i++)
                {
                    recvBody += cs[i] + "\n";
                }
            }
            else
            {

                //显示的消息过长就清理
                if (recvBody.Length >= 300)
                {
                    recvBody = "";
                }
                recvBody += str + "\n";
            }

            //继续接收消息
            socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
        }
        catch(Exception e)
        {
            recvText.text += "连接已断开";
            socket.Close();
        }
    }

    //发送消息
    public void Send()
    {
        Send(body.text);
    }

    public void Send(string body)
    {
        //str 客户端要发送的消息 bytes 转换成字节流
        string str = body;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        try
        {
            //Send 给服务端发送消息
            socket.Send(bytes);
        }
        catch
        {

        }
    }
}
