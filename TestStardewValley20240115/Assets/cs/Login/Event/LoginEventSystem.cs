using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// ��¼ע��ϵͳ�¼�������
/// </summary>
public class LoginEventSystem : MonoBehaviour
{
    /// <summary>
    /// ������Ϸ��������·��
    /// </summary>
    private static string savaGameDataPath;
    /// <summary>
    /// ���屣�����ݵ����ص��ļ����ڵ��ļ�������
    /// </summary>
    private const string savaGameDataFileName = "Login_Data";

    //����Ϸ��ʼʱ�����ݱ��淽�������ݼ��ط�����ӵ��¼����ĵ��¼�����
    private void OnEnable()
    {
        LoginEventHandler.SavaData += SavaData;
        LoginEventHandler.LoadData += LoadData;
    }
    //�����󱻽��û�������ʱ���¼���������ӵ��¼���Ӧ�Ƴ�
    private void OnDisable()
    {
        LoginEventHandler.SavaData -= SavaData;
        LoginEventHandler.LoadData -= LoadData;
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void Awake()
    {
        //PS:Application.persistentDataPath��õ���unity��Ϸ�����ļ��е�·��
        savaGameDataPath = Application.persistentDataPath;
        //debug��ӡ�������ݵ�·��
        Debug.Log(savaGameDataPath);
    }

    /// <summary>
    /// ���𱣴����� (�¼�)
    /// </summary>
    /// <param name="data">��Ҫ���������</param>
    private void SavaData(AccountData data)
    {
        //����Ƿ���ڸ��ļ���
        if (!Directory.Exists(savaGameDataPath + "/" + savaGameDataFileName))
        {
            //���û�и��ļ������ڸ�·���´���һ����Ϊlogin_Data���ļ���
            Directory.CreateDirectory(savaGameDataPath + "/" + savaGameDataFileName);
        }
        //ʹ��file��̬����Create��login_Data�ļ����´���һ����Ϊdata��txt�ļ� ���ҽ����ض����������Ա��������
        using (FileStream file = File.Create(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt"))
        {
            //������ת�� �ö�����Ҫ���ڶ�����ת������(���ڼ���)
            BinaryFormatter formatter = new BinaryFormatter();
            //ʹ��JsonUtility��̬����ToJson����(Data)����ת��ΪJson��ʽ������Ϊһ���ַ���
            string json = JsonUtility.ToJson(data);
            //ʹ��formatterʵ������Serialize(���л�)��json�ַ�������file�ļ���
            formatter.Serialize(file, json);
        }
    }

    /// <summary>
    /// ����������� (�¼�)
    /// </summary>
    /// <returns>���ؼ��ص�����</returns>
    private AccountData LoadData()
    {
        //��ʱ����,���ڱ����ȡ����Ϸ����
        AccountData data = new AccountData();
        //����Ƿ���ڸ��ļ�
        if (File.Exists(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt"))
        {
            //ʹ��File��̬����Open��ȡ�����ڱ��ص������ļ�
            using (FileStream file = File.Open(savaGameDataPath + "/" + savaGameDataFileName + "/data.txt", FileMode.Open))
            {
                //������ת�� �ö�����Ҫ���ڶ�����ת������(���ڽ���)
                BinaryFormatter formatter = new BinaryFormatter();
                //ʹ��formatterʵ������Deserialize(�����л�)���ļ��Ķ���������ת����Json�ı�����Json�ı����س�һ���ַ���
                string json = (string)formatter.Deserialize(file);
                //ʹ��JsonUtility��̬����FromJsonOverwrite��Json�ı�������д��data��
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }
        //����������˺������ļ�,�򴴽�һ����ʼ�˺�
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
        //��������
        return data;
    }

}
