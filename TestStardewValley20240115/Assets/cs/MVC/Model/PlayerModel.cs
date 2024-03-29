using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerModel
{
    //��������
    private string name;
    private int level;
    private int exp;
    private int gold;
    //����
    public string Name { get => name; }
    public int Level { get => level; }
    public int Exp { get => exp; }
    public int Gold { get => gold; }


    //���²���
    public void AddLeve()
    {
        level += 1;

        SavaData();
    }
    public void AddExp()
    {
        exp += 10;

        if (exp >= 100)
        {
            exp -= 100;
            level += 1;
        }

        SavaData();
    }
    public void AddGold()
    {
        gold += 100;

        SavaData();
    }


    //��ʼ��
    public void Init()
    {
        //��ȡ��������
        name = PlayerPrefs.GetString("Name", "awa");
        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 1);
        gold = PlayerPrefs.GetInt("Gold", 1);
    }
    //����
    public void SavaData()
    {
        //�������ݵ�����
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Exp", exp);
        PlayerPrefs.SetInt("Gold", gold);

        CallUpdateEvent();
    }


    //����ע����� ����֪ͨ����View��ȥ��������
    private event Action<PlayerModel> updateEvent;
    //ע���¼� һ��ע��͸���
    public void AddUpdateEvent(Action<PlayerModel> action)
    {
        updateEvent += action;
        CallUpdateEvent();
    }
    //֪ͨView�����UI
    public void CallUpdateEvent()
    {
        updateEvent?.Invoke(this);
    }


    //����ģʽ 1.����������Model�� 2.ͬʱ��֤ÿ��Model��Ψһ
    private static PlayerModel model;
    public static PlayerModel Instance
    {
        get
        {
            if (model == null)
            {
                model = new PlayerModel();
                model.Init();
            }
            return model;
        }
    }

}
