using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Model : Model<Player2Model>
{
    //数据内容
    private string name;
    private int level;
    private int exp;
    private int gold;
    //属性
    public string Name { get => name; }
    public int Level { get => level; }
    public int Exp { get => exp; }
    public int Gold { get => gold; }


    //更新操作
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


    //初始化
    public override void Init()
    {
        //读取本地数据
        name = PlayerPrefs.GetString("Name2", "qwq");
        level = PlayerPrefs.GetInt("Level2", 1);
        exp = PlayerPrefs.GetInt("Exp2", 1);
        gold = PlayerPrefs.GetInt("Gold2", 1);
    }
    //保存
    public void SavaData()
    {
        //保存数据到本地
        PlayerPrefs.SetString("Name2", name);
        PlayerPrefs.SetInt("Level2", level);
        PlayerPrefs.SetInt("Exp2", exp);
        PlayerPrefs.SetInt("Gold2", gold);

        CallUpdateEvent();
    }

}
