using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ArchiveManager : MonoBehaviour
{
    //需要保存与加载的数据
    public Data data = new Data();
    //需要保存与读取的游戏内数据
    public TMP_InputField text1;
    public TMP_InputField text2;
    public TMP_InputField text3;
    public TMP_InputField text4;
    //定义游戏保存数据路径
    public static string savaGameDataPath;
    //定义保存数据到本地的文件所在的文件夹名字
    //public static string savaGameDataFileName = "game_Data"; (写注释过于麻烦就不搞这个了)(忽略即可)
    private void Awake()
    {
        //PS:Application.persistentDataPath获得的是unity游戏数据文件夹的路径
        savaGameDataPath = Application.persistentDataPath;
        //debug打印保存数据的路径
        Debug.Log(savaGameDataPath);
    }
    /// <summary>
    /// 更新数据 (将游戏内数据保存以方便保存到本地)
    /// </summary>
    private void UpdateData()
    {
        data.data1 = text1.text;
        data.data2 = text2.text;
        data.data3 = text3.text;
        data.data4 = text4.text;
    }
    /// <summary>
    /// 更新游戏 (将数据保存到游戏中以方便游戏进行数据操作)
    /// </summary>
    private void UpdateGame()
    {
        text1.text = data.data1;
        text2.text = data.data2;
        text3.text = data.data3;
        text4.text = data.data4;
    }
    /// <summary>
    /// 保存游戏数据至本地
    /// </summary>
    public void SavaData()
    {
        //更新数据 方便后续保存数据至本地
        UpdateData();

        //检测是否存在该文件夹
        if (!Directory.Exists(savaGameDataPath + "/game_Data"))
        {
            //如果没有该文件夹则在该路径下创建一个名为game_Data的文件夹
            Directory.CreateDirectory(savaGameDataPath + "/game_Data");
        }
        //使用file静态方法Create在game_Data文件夹下创建一个名为data的txt文件 并且将返回对象存入变量以便后续操作
        using (FileStream file = File.Create(savaGameDataPath + "/game_Data/data.txt"))
        {
            //二进制转化 该对象主要用于二进制转化操作(用于加密)
            BinaryFormatter formatter = new BinaryFormatter();
            //使用JsonUtility静态方法ToJson将类(Data)数据转换为Json格式并返回为一个字符串
            string json = JsonUtility.ToJson(data);
            //使用formatter实例方法Serialize(序列化)将json字符串导入file文件中
            formatter.Serialize(file, json);
        }
    }

    public void LoadData()
    {
        //检测是否存在该文件
        if (File.Exists(savaGameDataPath + "/game_Data/data.txt"))
        {
            //使用File静态方法Open读取保存在本地的数据文件
            using (FileStream file = File.Open(savaGameDataPath+"/game_Data/data.txt",FileMode.Open))
            {
                //二进制转化 该对象主要用于二进制转化操作(用于解密)
                BinaryFormatter formatter = new BinaryFormatter();
                //使用formatter实例方法Deserialize(反序列化)将文件的二进制数据转换成Json文本并把Json文本返回成一个字符串
                string json = (string)formatter.Deserialize(file);
                //使用JsonUtility静态方法FromJsonOverwrite将Json文本的数据写入data中
                JsonUtility.FromJsonOverwrite(json, data);
            }
            //更新游戏 把本地数据更新到游戏中
            UpdateGame();
        }
    }

}