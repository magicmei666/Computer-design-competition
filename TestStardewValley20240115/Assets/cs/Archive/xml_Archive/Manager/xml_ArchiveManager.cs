using UnityEngine;
using TMPro;
using System.IO;
using System.Xml;

public class xml_ArchiveManager : MonoBehaviour
{
    //��Ҫ��������ص�����
    public Data data = new Data();
    //��Ҫ�������ȡ����Ϸ������
    public TMP_InputField text1;
    public TMP_InputField text2;
    public TMP_InputField text3;
    public TMP_InputField text4;
    //������Ϸ��������·��
    public static string savaGameDataPath;

    private void Awake()
    {
        //PS:Application.persistentDataPath��õ���unity��Ϸ�����ļ��е�·��
        savaGameDataPath = Application.persistentDataPath;
        //debug��ӡ�������ݵ�·��
        Debug.Log(savaGameDataPath);
    }

    /// <summary>
    /// �������� (����Ϸ�����ݱ����Է��㱣�浽����)
    /// </summary>
    private void UpdateData()
    {
        data.data1 = text1.text;
        data.data2 = text2.text;
        data.data3 = text3.text;
        data.data4 = text4.text;
    }

    /// <summary>
    /// ������Ϸ (�����ݱ��浽��Ϸ���Է�����Ϸ�������ݲ���)
    /// </summary>
    private void UpdateGame()
    {
        text1.text = data.data1;
        text2.text = data.data2;
        text3.text = data.data3;
        text4.text = data.data4;
    }

    /// <summary>
    /// ������Ϸ����������
    /// </summary>
    public void SavaData()
    {
        //�������� ���������������������
        UpdateData();

        //����Ƿ���ڸ��ļ���
        if (!Directory.Exists(savaGameDataPath + "/game_Data_xml"))
        {
            //���û�и��ļ������ڸ�·���´���һ����Ϊgame_Data���ļ���
            Directory.CreateDirectory(savaGameDataPath + "/game_Data_xml");
        }

        //ʵ����XmlDocument�ĵ��Ա����XmlDocumentʵ������
        XmlDocument xml = new XmlDocument();
        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ�
        XmlElement chen = xml.CreateElement("Chen");
        //ʹ��XmlElementʵ������SetAttribute���ýڵ�����
        chen.SetAttribute("Name", "���ڵ�");

        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ� (�浵ID�ڵ�)
        XmlElement id = xml.CreateElement("ArchiveID");
        //ʹ��XmlElementʵ������SetAttribute���ýڵ����� (���ô浵ID)
        id.SetAttribute("ID", "1");
        //���浵ID�ڵ���������ڵ��� ��Ϊ���ӽڵ�
        chen.AppendChild(id);

        #region �������ݲ���
        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ� (���ݽڵ�)
        XmlElement data1 = xml.CreateElement("data1");
        //�����ݽڵ�������浵ID�ڵ��� ��Ϊ���ӽڵ�
        id.AppendChild(data1);
        //ͨ��XmlElement��InnerText�������ýڵ���Ϣ (��������)
        data1.InnerText = text1.text;

        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ� (���ݽڵ�)
        XmlElement data2 = xml.CreateElement("data2");
        //�����ݽڵ�������浵ID�ڵ��� ��Ϊ���ӽڵ�
        id.AppendChild(data2);
        //ͨ��XmlElement��InnerText�������ýڵ���Ϣ (��������)
        data2.InnerText = text2.text;

        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ� (���ݽڵ�)
        XmlElement data3 = xml.CreateElement("data3");
        //�����ݽڵ�������浵ID�ڵ��� ��Ϊ���ӽڵ�
        id.AppendChild(data3);
        //ͨ��XmlElement��InnerText�������ýڵ���Ϣ (��������)
        data3.InnerText = text3.text;

        //ʹ��XmlDocumentʵ������CreateElement����һ���ڵ� (���ݽڵ�)
        XmlElement data4 = xml.CreateElement("data4");
        //�����ݽڵ�������浵ID�ڵ��� ��Ϊ���ӽڵ�
        id.AppendChild(data4);
        //ͨ��XmlElement��InnerText�������ýڵ���Ϣ (��������)
        data4.InnerText = text4.text;
        #endregion

        //�����ڵ������XmlDocument�ĵ���
        xml.AppendChild(chen);
        //��XmlDocument�ĵ����ݱ��浽·�������ļ���
        xml.Save(savaGameDataPath + "/game_Data_xml/" + "data.xml");
    }

    /// <summary>
    /// ��ȡ������������Ϸ
    /// </summary>
    public void LoadData()
    {
        //����Ƿ���ڸ��ļ�
        if (File.Exists(savaGameDataPath + "/game_Data_xml/data.xml"))
        {
            //ʹ��File��̬����Open��ȡ�����ڱ��ص������ļ�
            using (FileStream file = File.Open(savaGameDataPath + "/game_Data_xml/data.xml", FileMode.Open))
            {
                //ʵ����XmlDocument�ĵ��Ա����XmlDocumentʵ������
                XmlDocument xml = new XmlDocument();
                //ʹ��XmlDocumentʵ������Load����xml�ļ�����
                xml.Load(file);

                XmlNodeList xmlNodeList = xml.GetElementsByTagName("ArchiveID");

                //��Ҫ��ȡ�Ĵ浵��IDֵ
                string ID = "1";
                //��ȡ���д浵��Ϣ
                foreach (XmlNode item in xmlNodeList)
                {
                    //�ж��Ƿ�����Ҫ�Ĵ浵
                    if(ID == item.Attributes[0].Value)
                    {
                        //����Ӧ�浵ID�ڵ��µ��ӽڵ���Ϣ��������
                        data.data1 = item.ChildNodes[0].InnerText;
                        data.data2 = item.ChildNodes[1].InnerText;
                        data.data3 = item.ChildNodes[2].InnerText;
                        data.data4 = item.ChildNodes[3].InnerText;
                    }
                }
            }
            //������Ϸ �ѱ������ݸ��µ���Ϸ��
            UpdateGame();
        }
    }

}