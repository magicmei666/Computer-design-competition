using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排行榜类
/// </summary>
public class Rankings : MonoBehaviour
{
    [SerializeField]
    //保存所有账号的名字
    private List<string> accountsName;
    //排行榜的显示文本
    private TMPro.TMP_Text text;
    //保存数据库实例
    private DataBase dataBase;

    //当对象被激活时调用一次
    private void OnEnable()
    {
        //获得管理类的数据库实例
        dataBase = LoginManager.Instance.dataBase;
        //将文本框文本清空
        text.text = string.Empty;
        //获得所有账号的名字
        accountsName = dataBase.GetAllAccountName();
        //获得排序后的账号列表
        accountsName = SortList(accountsName);

        UpdateView(accountsName);
    }

    private void Awake()
    {
        text = transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>();
    }

    /// <summary>
    /// 直接插入算法
    /// </summary>
    /// <param name="list">需要排序的列表</param>
    public List<string> SortList(List<string> list)
    {
        //首先从第二个元素开始遍历
        for (int i = 1; i < list.Count; i++)
        {
            string value = list[i];
            bool isMin = true;
            //让 i 下标前的全部数据和 i下标数据进行比较
            for (int j = i - 1; j >= 0; j--)
            {
                if (dataBase.GetAccountValue(list[j]) < dataBase.GetAccountValue(value))//只要比i下标数据大就向后移一位下标，
                {
                    list[j + 1] = list[j];
                }
                else//如果比i下标数据小或者相等，那么就把i下标数据赋值到他后面
                {
                    list[j + 1] = value;
                    isMin = false;
                    break;//这里需要跳出循环，因为前面的值都是比i下标数据小
                }
            }
            if (isMin)//如果遍历完了，前面的数据都比我大，那i下标数据就到第一个位置去
            {
                list[0] = value;
            }
        }
        return list;
    }

    public void UpdateView(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //显示分数前十的玩家
            if (i < 10)
            {
                text.text += "第" + (i+1) + "名 玩家:" + list[i] + " 分数:" + dataBase.GetAccountValue(list[i]) + "\n";
            }
            else
            {
                break;
            }
        }

        string name = PlayerPrefs.GetString("csName", "null");
        for (int i = 0; i < list.Count; i++)
        {
            if (name == "null")
            {
                text.text += "你目前还未登录,无法查询到排行;";
                break;
            }
            if (list[i] == name)
            {
                text.text += "你当前排名:" + (i+1) + " (玩家:" + list[i] + ")";
            }
        }
    }
}
