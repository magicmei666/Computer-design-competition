using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerModel
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


    //初始化
    public void Init()
    {
        //读取本地数据
        name = PlayerPrefs.GetString("Name", "awa");
        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 1);
        gold = PlayerPrefs.GetInt("Gold", 1);
    }
    //保存
    public void SavaData()
    {
        //保存数据到本地
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Exp", exp);
        PlayerPrefs.SetInt("Gold", gold);

        CallUpdateEvent();
    }


    //建立注册机制 用于通知所有View层去更新数据
    private event Action<PlayerModel> updateEvent;
    //注册事件 一旦注册就更新
    public void AddUpdateEvent(Action<PlayerModel> action)
    {
        updateEvent += action;
        CallUpdateEvent();
    }
    //通知View层更新UI
    public void CallUpdateEvent()
    {
        updateEvent?.Invoke(this);
    }


    //单例模式 1.方便外面获得Model层 2.同时保证每个Model层唯一
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
