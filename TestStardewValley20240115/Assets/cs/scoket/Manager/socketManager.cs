using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;

public class socketManager : MonoBehaviour
{
    //�����˵Ľ�����
    Socket socket;
    //����˵�ip��˿�
    public InputField host;
    public InputField port;
    //Ҫ���͵���Ϣ
    public InputField body;
    //��ʾ����
    public Text recvText;
    public Text clienText;
    //���ջ�����
    const int BUFFER_SIZE = 1024;
    byte[] readBuff = new byte[BUFFER_SIZE];
    //��ʾ�������ַ���
    public string recvBody = string.Empty;

    //ֻ�����̲߳����޸�UI���������
    //��ν����ݱ������ַ���,Ȼ���޸�ui�ı�
    private void Update()
    {
        //��ʾ��Ϣ
        recvText.text = recvBody;
    }

    //���ӷ�����
    public void Connetion()
    {
        //����ʱ����ԭ������ʾ
        recvText.text = "";

        //Socket �����ͻ��˽�����
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //�����ip��˿�
        string host = this.host.text;
        int port = int.Parse(this.port.text);

        //Connect ���ӷ����
        socket.Connect(host, port);
        clienText.text = "�ͻ��˵�ַ:" + socket.LocalEndPoint.ToString();

        Send("GetMsg");

        //Receive ���շ������Ϣ count ��Ϣ����
        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
    }

    //���ջص�
    public void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            //count���ݴ�С
            int count = socket.EndReceive(ar);
            //������յ�����
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

                //��ʾ����Ϣ����������
                if (recvBody.Length >= 300)
                {
                    recvBody = "";
                }
                recvBody += str + "\n";
            }

            //����������Ϣ
            socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
        }
        catch(Exception e)
        {
            recvText.text += "�����ѶϿ�";
            socket.Close();
        }
    }

    //������Ϣ
    public void Send()
    {
        Send(body.text);
    }

    public void Send(string body)
    {
        //str �ͻ���Ҫ���͵���Ϣ bytes ת�����ֽ���
        string str = body;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        try
        {
            //Send ������˷�����Ϣ
            socket.Send(bytes);
        }
        catch
        {

        }
    }
}
