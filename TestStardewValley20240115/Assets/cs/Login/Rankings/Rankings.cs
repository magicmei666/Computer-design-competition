using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���а���
/// </summary>
public class Rankings : MonoBehaviour
{
    [SerializeField]
    //���������˺ŵ�����
    private List<string> accountsName;
    //���а����ʾ�ı�
    private TMPro.TMP_Text text;
    //�������ݿ�ʵ��
    private DataBase dataBase;

    //�����󱻼���ʱ����һ��
    private void OnEnable()
    {
        //��ù���������ݿ�ʵ��
        dataBase = LoginManager.Instance.dataBase;
        //���ı����ı����
        text.text = string.Empty;
        //��������˺ŵ�����
        accountsName = dataBase.GetAllAccountName();
        //����������˺��б�
        accountsName = SortList(accountsName);

        UpdateView(accountsName);
    }

    private void Awake()
    {
        text = transform.GetChild(0).GetChild(2).GetComponent<TMPro.TMP_Text>();
    }

    /// <summary>
    /// ֱ�Ӳ����㷨
    /// </summary>
    /// <param name="list">��Ҫ������б�</param>
    public List<string> SortList(List<string> list)
    {
        //���ȴӵڶ���Ԫ�ؿ�ʼ����
        for (int i = 1; i < list.Count; i++)
        {
            string value = list[i];
            bool isMin = true;
            //�� i �±�ǰ��ȫ�����ݺ� i�±����ݽ��бȽ�
            for (int j = i - 1; j >= 0; j--)
            {
                if (dataBase.GetAccountValue(list[j]) < dataBase.GetAccountValue(value))//ֻҪ��i�±����ݴ�������һλ�±꣬
                {
                    list[j + 1] = list[j];
                }
                else//�����i�±�����С������ȣ���ô�Ͱ�i�±����ݸ�ֵ��������
                {
                    list[j + 1] = value;
                    isMin = false;
                    break;//������Ҫ����ѭ������Ϊǰ���ֵ���Ǳ�i�±�����С
                }
            }
            if (isMin)//����������ˣ�ǰ������ݶ����Ҵ���i�±����ݾ͵���һ��λ��ȥ
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
            //��ʾ����ǰʮ�����
            if (i < 10)
            {
                text.text += "��" + (i+1) + "�� ���:" + list[i] + " ����:" + dataBase.GetAccountValue(list[i]) + "\n";
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
                text.text += "��Ŀǰ��δ��¼,�޷���ѯ������;";
                break;
            }
            if (list[i] == name)
            {
                text.text += "�㵱ǰ����:" + (i+1) + " (���:" + list[i] + ")";
            }
        }
    }
}
