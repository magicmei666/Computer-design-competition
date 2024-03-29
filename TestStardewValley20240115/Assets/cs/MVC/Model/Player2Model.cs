using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Model : Model<Player2Model>
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
        exp += 20;

        if (exp >= 100)
        {
            exp -= 100;
            level += 1;
        }

        SavaData();
    }
    public void AddGold()
    {
        gold += 60;

        SavaData();
    }


    //��ʼ��
    public override void Init()
    {
        //��ȡ��������
        name = PlayerPrefs.GetString("Name2", "qwq");
        level = PlayerPrefs.GetInt("Level2", 1);
        exp = PlayerPrefs.GetInt("Exp2", 1);
        gold = PlayerPrefs.GetInt("Gold2", 1);
    }
    //����
    public void SavaData()
    {
        //�������ݵ�����
        PlayerPrefs.SetString("Name2", name);
        PlayerPrefs.SetInt("Level2", level);
        PlayerPrefs.SetInt("Exp2", exp);
        PlayerPrefs.SetInt("Gold2", gold);

        CallUpdateEvent();
    }

}
