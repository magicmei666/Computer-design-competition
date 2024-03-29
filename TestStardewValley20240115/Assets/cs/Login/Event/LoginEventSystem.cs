using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 登录注册系统事件管理类
/// </summary>
public class LoginEventSystem : MonoBehaviour
{
    /// <summary>
    /// 定义游戏保存数据路径
    /// </summary>
    private static string savaGameDataPath;
    /// <summary>
    /// 定义保存数据到本地的文件所在的文件夹名字
    /// </summary>
    private const string savaGameDataFileName = "Login_Data";

    //当游戏开始时将数据保存方法与数据加载方法添加到事件中心的事件当中
    private void OnEnable()
    {
        LoginEventHandler.SavaData += SavaData;
        LoginEventHandler.LoadData += LoadData;
    }
    //当对象被禁用或者销毁时将事件中心中添加的事件对应移除
    private void OnDisable()
    {
        LoginEventHandler.SavaData -= SavaData;
        LoginEventHandler.LoadData -= LoadData;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        //PS:Application.persistentDataPath获得的是unity游戏数据文件夹的路径
        savaGameDataPath = Application.persistentDataPath;
        //debug打印保存数据的路径
        Debug.Log(savaGameDataPath);
    }

    /// <summary>
    /// 负责保存数据 (事件)
    /// </summary>
    /// <param name="data">需要保存的数据</param>
    private void SavaData(AccountData data)
    {
        //检测是否存在该文件夹
        if (!Directory.Exists(savaGameDataPath + "/" + savaGameDataFileName))
        {
            //如果没有该文件夹则在该路径下创建一个名为login_Data的文件夹
            Directory.CreateDirectory(savaGameDataPath + "/" + savaGameDataFileName);
        }
        //使用file静态方法Create在login_Data文件夹下创建一个名为data的txt文件 并且将返回对象存入变量以便后续操作
        using (FileStream file = File.Create(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt"))
        {
            //二进制转化 该对象主要用于二进制转化操作(用于加密)
            BinaryFormatter formatter = new BinaryFormatter();
            //使用JsonUtility静态方法ToJson将类(Data)数据转换为Json格式并返回为一个字符串
            string json = JsonUtility.ToJson(data);
            //使用formatter实例方法Serialize(序列化)将json字符串导入file文件中
            formatter.Serialize(file, json);
        }
    }

    /// <summary>
    /// 负责加载数据 (事件)
    /// </summary>
    /// <returns>返回加载的数据</returns>
    private AccountData LoadData()
    {
        //临时变量,用于保存读取的游戏数据
        AccountData data = new AccountData();
        //检测是否存在该文件
        if (File.Exists(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt"))
        {
            //使用File静态方法Open读取保存在本地的数据文件
            using (FileStream file = File.Open(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt", FileMode.Open))
            {
                //二进制转化 该对象主要用于二进制转化操作(用于解密)
                BinaryFormatter formatter = new BinaryFormatter();
                //使用formatter实例方法Deserialize(反序列化)将文件的二进制数据转换成Json文本并把Json文本返回成一个字符串
                string json = (string)formatter.Deserialize(file);
                //使用JsonUtility静态方法FromJsonOverwrite将Json文本的数据写入data中
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }
        //如果不存在账号数据文件,则创建一个初始账号
        else
        {
            data = new AccountData();
            data.data.Add(new Account()
            {
                Name = "Admin",
                Password = "123456",
                Value = 12356
            });
        }
        //返回数据
        return data;
    }

}
