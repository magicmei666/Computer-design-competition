using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using UnityEngine.UI;
public class Mail : MonoBehaviour
{
    public InputField mail;
    public InputField yzm;

    public Text jg;

    public string yzmSava;

    //��ȡ��֤��
    public void GetYzm()
    {
        //������֤��
        yzmSava = UnityEngine.Random.Range(100000, 999999).ToString();

        if (mail.text != string.Empty)
        {
            Send(mail.text, "��Ϸ��֤��(����)", "[������Ϸ]��֤����:"+yzmSava);
            jg.text = "��֤���ѷ���";
        }
        else
        {
            jg.text = "���䲻��Ϊ��";
        }
    }

    //��֤��֤��
    public void IsYzm()
    {
        if(yzm.text == yzmSava)
        {
            jg.text = "��֤��:��ȷ";
        }
        else
        {
            jg.text = "��֤��:����";
        }
    }

    //�����ʼ�
    void Send(string player, string title, string body)
    {
        // ����SMTP��������Ϣ
        string smtpServer = "smtp.qq.com"; // ���SMTP��������ַ
        int smtpPort = 587; // ���SMTP�������˿ڣ�ͨ����25, 465, 587��
        string smtpUser = "chenfengsas@qq.com"; // ��ĵ����ʼ���ַ
        string smtpPassword = "riqmeukkhxmjdjfb"; // ��ĵ����ʼ������Ӧ��ר������

        // �����ʼ�����
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(smtpUser); // �����˵�ַ
        mail.To.Add(player); // �ռ��˵�ַ
        mail.Subject = title; // �ʼ�����
        mail.Body = body; // �ʼ�����

        // ����SMTP�ͻ���
        SmtpClient client = new SmtpClient
        {
            Host = smtpServer,
            Port = smtpPort,
            EnableSsl = true, // ������SMTP��������ҪSSL��������Ϊtrue
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(smtpUser, smtpPassword),
            Timeout = 20000,
        };

        try
        {
            // �����ʼ�
            client.Send(mail);
            jg.text = "�ʼ����ͳɹ���";
        }
        catch (SmtpException ex)
        {
           jg.text = "�ʼ�����ʧ��: " + ex.Message;
        }
    }

}