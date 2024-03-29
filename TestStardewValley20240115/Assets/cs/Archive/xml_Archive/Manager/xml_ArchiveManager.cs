using UnityEngine;
using TMPro;
using System.IO;
using System.Xml;

public class xml_ArchiveManager : MonoBehaviour
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
        if (!Directory.Exists(savaGameDataPath + "/game_Data_xml"))
        {
            //如果没有该文件夹则在该路径下创建一个名为game_Data的文件夹
            Directory.CreateDirectory(savaGameDataPath + "/game_Data_xml");
        }

        //实例化XmlDocument文档以便调用XmlDocument实例方法
        XmlDocument xml = new XmlDocument();
        //使用XmlDocument实例方法CreateElement创建一个节点
        XmlElement chen = xml.CreateElement("Chen");
        //使用XmlElement实例方法SetAttribute设置节点属性
        chen.SetAttribute("Name", "主节点");

        //使用XmlDocument实例方法CreateElement创建一个节点 (存档ID节点)
        XmlElement id = xml.CreateElement("ArchiveID");
        //使用XmlElement实例方法SetAttribute设置节点属性 (设置存档ID)
        id.SetAttribute("ID", "1");
        //将存档ID节点添加至主节点中 成为其子节点
        chen.AppendChild(id);

        #region 保存数据部分
        //使用XmlDocument实例方法CreateElement创建一个节点 (数据节点)
        XmlElement data1 = xml.CreateElement("data1");
        //将数据节点添加至存档ID节点中 成为其子节点
        id.AppendChild(data1);
        //通过XmlElement的InnerText属性设置节点信息 (数据内容)
        data1.InnerText = text1.text;

        //使用XmlDocument实例方法CreateElement创建一个节点 (数据节点)
        XmlElement data2 = xml.CreateElement("data2");
        //将数据节点添加至存档ID节点中 成为其子节点
        id.AppendChild(data2);
        //通过XmlElement的InnerText属性设置节点信息 (数据内容)
        data2.InnerText = text2.text;

        //使用XmlDocument实例方法CreateElement创建一个节点 (数据节点)
        XmlElement data3 = xml.CreateElement("data3");
        //将数据节点添加至存档ID节点中 成为其子节点
        id.AppendChild(data3);
        //通过XmlElement的InnerText属性设置节点信息 (数据内容)
        data3.InnerText = text3.text;

        //使用XmlDocument实例方法CreateElement创建一个节点 (数据节点)
        XmlElement data4 = xml.CreateElement("data4");
        //将数据节点添加至存档ID节点中 成为其子节点
        id.AppendChild(data4);
        //通过XmlElement的InnerText属性设置节点信息 (数据内容)
        data4.InnerText = text4.text;
        #endregion

        //将主节点添加至XmlDocument文档中
        xml.AppendChild(chen);
        //将XmlDocument文档内容保存到路径所在文件中
        xml.Save(savaGameDataPath + "/game_Data_xml/" + "data.xml");
    }

    /// <summary>
    /// 读取本地数据至游戏
    /// </summary>
    public void LoadData()
    {
        //检测是否存在该文件
        if (File.Exists(savaGameDataPath + "/game_Data_xml/data.xml"))
        {
            //使用File静态方法Open读取保存在本地的数据文件
            using (FileStream file = File.Open(savaGameDataPath + "/game_Data_xml/data.xml", FileMode.Open))
            {
                //实例化XmlDocument文档以便调用XmlDocument实例方法
                XmlDocument xml = new XmlDocument();
                //使用XmlDocument实例方法Load加载xml文件内容
                xml.Load(file);

                XmlNodeList xmlNodeList = xml.GetElementsByTagName("ArchiveID");

                //需要读取的存档的ID值
                string ID = "1";
                //读取所有存档信息
                foreach (XmlNode item in xmlNodeList)
                {
                    //判断是否是需要的存档
                    if(ID == item.Attributes[0].Value)
                    {
                        //将对应存档ID节点下的子节点信息保存下来
                        data.data1 = item.ChildNodes[0].InnerText;
                        data.data2 = item.ChildNodes[1].InnerText;
                        data.data3 = item.ChildNodes[2].InnerText;
                        data.data4 = item.ChildNodes[3].InnerText;
                    }
                }
            }
            //更新游戏 把本地数据更新到游戏中
            UpdateGame();
        }
    }

}