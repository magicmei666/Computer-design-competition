using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ArchiveManager : MonoBehaviour
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
    //���屣�����ݵ����ص��ļ����ڵ��ļ�������
    //public static string savaGameDataFileName = "game_Data"; (дע�͹����鷳�Ͳ��������)(���Լ���)
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
        if (!Directory.Exists(savaGameDataPath + "/game_Data"))
        {
            //���û�и��ļ������ڸ�·���´���һ����Ϊgame_Data���ļ���
            Directory.CreateDirectory(savaGameDataPath + "/game_Data");
        }
        //ʹ��file��̬����Create��game_Data�ļ����´���һ����Ϊdata��txt�ļ� ���ҽ����ض����������Ա��������
        using (FileStream file = File.Create(savaGameDataPath + "/game_Data/data.txt"))
        {
            //������ת�� �ö�����Ҫ���ڶ�����ת������(���ڼ���)
            BinaryFormatter formatter = new BinaryFormatter();
            //ʹ��JsonUtility��̬����ToJson����(Data)����ת��ΪJson��ʽ������Ϊһ���ַ���
            string json = JsonUtility.ToJson(data);
            //ʹ��formatterʵ������Serialize(���л�)��json�ַ�������file�ļ���
            formatter.Serialize(file, json);
        }
    }

    public void LoadData()
    {
        //����Ƿ���ڸ��ļ�
        if (File.Exists(savaGameDataPath + "/game_Data/data.txt"))
        {
            //ʹ��File��̬����Open��ȡ�����ڱ��ص������ļ�
            using (FileStream file = File.Open(savaGameDataPath+"/game_Data/data.txt",FileMode.Open))
            {
                //������ת�� �ö�����Ҫ���ڶ�����ת������(���ڽ���)
                BinaryFormatter formatter = new BinaryFormatter();
                //ʹ��formatterʵ������Deserialize(�����л�)���ļ��Ķ���������ת����Json�ı�����Json�ı����س�һ���ַ���
                string json = (string)formatter.Deserialize(file);
                //ʹ��JsonUtility��̬����FromJsonOverwrite��Json�ı�������д��data��
                JsonUtility.FromJsonOverwrite(json, data);
            }
            //������Ϸ �ѱ������ݸ��µ���Ϸ��
            UpdateGame();
        }
    }

}