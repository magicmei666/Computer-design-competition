using UnityEngine;
using TMPro;

/// <summary>
/// ��¼ע��ϵͳ������
/// </summary>
public class LoginManager : MonoBehaviour
{
    //---��Ϸ����---//
    private GameObject loginBefore;
    private GameObject loginAfter;
    //---��¼�˺���ע���˺Ž���---//
    private GameObject login;
    private GameObject erllon;
    //---��¼�˺����������Լ����������---//
    private TMP_InputField loginName;
    private TMP_InputField loginPwd;
    //---ע���˺����������Լ����������---//
    private TMP_InputField erllonName;
    private TMP_InputField erllonPwd;
    //---�Ի���---//
    private PromptBox promptBox;
    //---���ݿ�ʵ��---//
    public DataBase dataBase;

    /// <summary>
    /// ��ó����е������������ֵ
    /// </summary>
    private void Awake()
    {
        Singleton = this;

        loginBefore = transform.parent.GetChild(2).gameObject;
        loginAfter = transform.parent.GetChild(3).gameObject;

        login = transform.parent.GetChild(4).GetChild(0).gameObject;
        erllon = transform.parent.GetChild(4).GetChild(1).gameObject;

        promptBox = transform.parent.GetChild(4).GetChild(2).GetComponent<PromptBox>();

        loginName = transform.parent.GetChild(4).GetChild(0).GetChild(1).GetComponent<TMP_InputField>();
        loginPwd = transform.parent.GetChild(4).GetChild(0).GetChild(2).GetComponent<TMP_InputField>();

        erllonName = transform.parent.GetChild(4).GetChild(1).GetChild(1).GetComponent<TMP_InputField>();
        erllonPwd = transform.parent.GetChild(4).GetChild(1).GetChild(2).GetComponent<TMP_InputField>();
    }

    /// <summary>
    /// ���س�ʼ��
    /// </summary>
    private void Start()
    {
        //ʵ�������ݿ�
        dataBase = new DataBase();
        //��ʼ����Ϸ
        Init();
    }

    /// <summary>
    /// ������������ݳ�ʼ��
    /// </summary>
    /// <returns>���ؼ��سɹ����������</returns>
    private void Init()
    {
        //��ȡ�����˺���Ϣ(������״ζ�ȡ���ʼ��Ϊnull)
        string name = PlayerPrefs.GetString("csName", "null");
        string password = PlayerPrefs.GetString("csPwd", "null");

        //�жϱ����ڱ��ص��˺������Ƿ���ȷ
        if (!dataBase.IsPassword(name, password) || name == "null")
        {
            loginBefore.SetActive(true);
            loginAfter.SetActive(false);
        }
        else
        {
            loginBefore.SetActive(false);
            loginAfter.SetActive(true);
        }

        //����¼�˺���ע���˺Ž���ر�
        login.SetActive(false);
        erllon.SetActive(false);
    }

    //--------------------��װ����--------------------//

    /// <summary>
    /// ��¼�˺ŷ���
    /// </summary>
    /// <param name="name">������˺�����</param>
    /// <param name="password">������˺�����</param>
    /// <param name="promptBox">��ʾ��</param>
    private void LoginAccount(TMP_InputField name, TMP_InputField password, PromptBox promptBox)
    {
        //ʹ��Ԫ�� ���ٸ�ֵ ͨ��Ԫ������ ��ֳɶ������
        var (_name, _password, _promptBox) = (name.text, password.text,promptBox);

        //���������Ƿ�Ϊ��
        if (_name == string.Empty || _password == string.Empty)
        {
            //�����Ϊ����ʾ
            _promptBox.ShowBox("����򲻿�Ϊ��");
            return;
        }
        //����Ƿ���ڸ��˺�
        if (dataBase.IsName(_name))
        {
            //��������Ƿ���ȷ
            if(dataBase.IsPassword(_name,_password))
            {
                //---������ȷִ��ʣ�²���---//
                //���Ѿ���¼���˺����뱣�浽����
                PlayerPrefs.SetString("csName", _name);
                PlayerPrefs.SetString("csPwd", _password);
                PlayerPrefs.SetInt("csValue", dataBase.GetAccountValue(_name));
                //---������Ϸ����---//
                loginBefore.SetActive(false);
                loginAfter.SetActive(true);
                login.SetActive(false);
                erllon.SetActive(false);
                //��¼�ɹ���ʾ
                _promptBox.ShowBox("��¼�ɹ�");
                return;
            }
            //���������ʾ
            _promptBox.ShowBox("�˺Ų����ڻ����������");
            return;
        }
        //���˺Ų�����ʱ
        else
        {
            //�˺Ų�������ʾ
            _promptBox.ShowBox("�˺Ų����ڻ����������");
            return;
        }
    }
    
    /// <summary>
    /// ע���˺ŷ���
    /// </summary>
    /// <param name="name">������˺�����</param>
    /// <param name="password">������˺�����</param>
    /// <param name="promptBox">��ʾ��</param>
    private void ErllonAccount(TMP_InputField name, TMP_InputField password, PromptBox promptBox)
    {
        var (_name, _password, _promptBox) = (name.text, password.text, promptBox);

        //���������Ƿ�Ϊ��
        if (_name == string.Empty || _password == string.Empty)
        {
            //�����Ϊ����ʾ
            _promptBox.ShowBox("����򲻿�Ϊ��");
            return;
        }
        //����Ƿ񲻴��ڸ��˺�
        if (!dataBase.IsName(_name))
        {
            //������ʱ�˺���Ϣ
            Account account = new Account()
            {
                Name = _name,
                Password = _password,
                Value = 0
            };
            //����˺�����Ϸ������
            dataBase.Add(account);
            //---������Ϸ����---//
            login.SetActive(false);
            erllon.SetActive(false);
            //ע��ɹ���ʾ
            _promptBox.ShowBox("ע��ɹ�");
            return;
        }
        //���˺Ŵ���ʱ
        else
        {
            //�˺Ŵ�����ʾ
            _promptBox.ShowBox("��ǰ�˺��Ѵ���");
            return;
        }
    }

    /// <summary>
    /// �Ի�����ʾ
    /// </summary>
    /// <param name="promptBox">��Ҫ��ʾ�ĶԻ���</param>
    /// <param name="body">�Ի�����ʾ������</param>
    private void ShowBox(PromptBox promptBox, string body)
    {
        promptBox.ShowBox(body);
    }

    //--------------------��ť�¼�����--------------------//

    /// <summary>
    /// ��¼�˺ŷ���
    /// </summary>
    public void LoginAccount()
    {
        LoginAccount(loginName, loginPwd, promptBox);
    }

    /// <summary>
    /// ע���˺ŷ���
    /// </summary>
    public void ErllonAccount()
    {
        ErllonAccount(erllonName, erllonPwd, promptBox);
    }

    /// <summary>
    /// �˳���Ϸ����
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// �˳���¼����
    /// </summary>
    public void QuitLogin()
    {
        //---����������ɾ��---//
        PlayerPrefs.SetString("csName", "null");
        PlayerPrefs.SetString("csPwd", "null");
        PlayerPrefs.SetInt("csValue", 0);
        //---������Ϸ����---//
        loginBefore.SetActive(true);
        loginAfter.SetActive(false);
        login.SetActive(false);
        erllon.SetActive(false);
    }

    /// <summary>
    /// �Ի�����ʾ
    /// </summary>
    /// <param name="body">�Ի�����ʾ������</param>
    public void ShowBox(string body)
    {
        ShowBox(promptBox, body);
    }

    //--------------------�����൥��ģʽ--------------------//

    private static LoginManager Singleton;

    [System.Obsolete]
    public static LoginManager Instance
    {
        get
        {
            if (Singleton != null)
                return Singleton;

            // ����Unity�ĸ��½��飬ʹ���µĲ��ҷ���
            Singleton = FindObjectOfType<LoginManager>();

            if (Singleton == null)
            {
                Singleton = new GameObject("loginManager").AddComponent<LoginManager>();
            }
            else
            {
                var items = FindObjectsOfType<LoginManager>();
                foreach (var item in items)
                {
                    if (item != Singleton)
                        Destroy(item.gameObject);
                }
            }
            return Singleton;
        }
    }

    // ע�⣺�˴��� FindObjectOfType �� FindObjectsOfType ������Ҫ�滻Ϊ�µ�API����
    // ʵ�ʵ��滻����ȡ�������Unity�汾�͹ٷ��ĵ������½���

}
