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

    //获取验证码
    public void GetYzm()
    {
        //生成验证码
        yzmSava = UnityEngine.Random.Range(100000, 999999).ToString();

        if (mail.text != string.Empty)
        {
            Send(mail.text, "游戏验证码(标题)", "[尘风游戏]验证码是:"+yzmSava);
            jg.text = "验证码已发送";
        }
        else
        {
            jg.text = "邮箱不可为空";
        }
    }

    //验证验证码
    public void IsYzm()
    {
        if(yzm.text == yzmSava)
        {
            jg.text = "验证码:正确";
        }
        else
        {
            jg.text = "验证码:错误";
        }
    }

    //发送邮件
    void Send(string player, string title, string body)
    {
        // 设置SMTP服务器信息
        string smtpServer = "smtp.qq.com"; // 你的SMTP服务器地址
        int smtpPort = 587; // 你的SMTP服务器端口，通常是25, 465, 587等
        string smtpUser = "chenfengsas@qq.com"; // 你的电子邮件地址
        string smtpPassword = "riqmeukkhxmjdjfb"; // 你的电子邮件密码或应用专用密码

        // 创建邮件对象
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(smtpUser); // 发件人地址
        mail.To.Add(player); // 收件人地址
        mail.Subject = title; // 邮件主题
        mail.Body = body; // 邮件正文

        // 设置SMTP客户端
        SmtpClient client = new SmtpClient
        {
            Host = smtpServer,
            Port = smtpPort,
            EnableSsl = true, // 如果你的SMTP服务器需要SSL，则设置为true
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(smtpUser, smtpPassword),
            Timeout = 20000,
        };

        try
        {
            // 发送邮件
            client.Send(mail);
            jg.text = "邮件发送成功！";
        }
        catch (SmtpException ex)
        {
           jg.text = "邮件发送失败: " + ex.Message;
        }
    }

}