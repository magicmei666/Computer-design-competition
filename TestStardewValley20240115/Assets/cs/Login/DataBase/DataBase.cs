using System.Collections.Generic;
/// <summary>
/// ���ݿ��� (���ڱ������ȡ��������)
/// </summary>
public class DataBase
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    private AccountData data = new AccountData();

    public DataBase()
    {
        data = LoginEventHandler.CallLoadData();
    }

    ~DataBase()
    {
        LoginEventHandler.CallSavaData(data);
    }

    //--------------------��ɾ�Ĳ鲿��(��������)--------------------//

    /// <summary>
    /// ����˺�
    /// </summary>
    /// <param name="account">��Ҫ��ӵ��˺���Ϣ</param>
    public bool Add(Account account)
    {
        if (!IsName(account.Name))
        {
            Add(account.Name,account.Password,account.Value);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ɾ���˺�
    /// </summary>
    /// <param name="name">��Ҫɾ�����˺���Ϣ</param>
    public bool Remove(Account account)
    {
        if (IsPassword(account.Name, account.Password))
        {
            Remove(account.Name);
            return true;
        }
        return false;
    }

    /// <summary>
    /// �޸��˺�
    /// </summary>
    /// <param name="account">��Ҫ�޸ĵ��˺�</param>
    /// <param name="account1">��Ҫ�޸ĳɵ��˺���Ϣ</param>
    public bool ReviseAccount(Account account, Account account1)
    {
        if (IsPassword(account.Name, account.Password))
        {
            ReviseAccount(account.Name, account1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ��������˺ŵ�����
    /// </summary>
    /// <returns>���������˺���������</returns>
    public List<string> GetAllAccountName()
    {
        List<string> vs = new List<string>();

        for (int i = 0; i < data.data.Count; i++)
        {
            vs.Add(data.data[i].Name);
        }

        return vs;
    }

    //--------------------��ɾ�Ĳ鲿��(��ȫ����)--------------------//

    /// <summary>
    /// ����˺�
    /// </summary>
    /// <param name="name">�˺�����</param>
    /// <param name="password">�˺�����</param>
    /// <returns>������ӽ��</returns>
    private bool Add(string name, string password)
    {
        if (IsName(name))
        {
            Add(name, password, 0);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ɾ���˺�
    /// </summary>
    /// <param name="name">��Ҫɾ�����˺�����</param>
    /// <param name="password">��Ҫɾ�����˺�����</param>
    public bool Remove(string name, string password)
    {
        if (IsPassword(name, password))
        {
            Remove(name);
            return true;
        }
        return false;
    }

    /// <summary>
    /// �����˺�
    /// </summary>
    /// <param name="name">��Ҫ���ҵ��˺�����</param>
    /// <param name="password">��Ҫ���ҵ��˺�����</param>
    /// <returns>���ز��ҽ��</returns>
    public Account GetAccount(string name, string password)
    {
        if (IsPassword(name, password))
        {
            return GetAccount(name);
        }
        return null;
    }

    /// <summary>
    /// �鿴���˺ŵ���Դ
    /// </summary>
    /// <param name="name">��Ҫ�鿴���˺�����</param>
    /// <param name="password">��Ҫ�鿴������</param>
    /// <returns>�����˺�ֵ</returns>
    public int GetAccountValue(string name)
    {
        if (IsName(name))
        {
            return GetAccount(name).Value;
        }
        return 0;
    }

    //--------------------��ɾ�Ĳ鲿��(˽�ܲ���)--------------------//

    /// <summary>
    /// ����˺�
    /// </summary>
    /// <param name="name">�˺�����</param>
    /// <param name="password">�˺ŵ�����</param>
    /// <param name="value">�˺���Դ</param>
    private void Add(string name, string password,int value)
    {
        data.data.Add(new Account()
        {
            Name = name,
            Password = password,
            Value = value
        });
        LoginEventHandler.CallSavaData(data);
    }

    /// <summary>
    /// ɾ���˺�
    /// </summary>
    /// <param name="name">��Ҫɾ�����˺�����</param>
    private void Remove(string name)
    {
        data.data.Remove(this[name]);
    }

    /// <summary>
    /// �޸��˺�
    /// </summary>
    /// <param name="name">��Ҫ�޸ĵ��˺�����</param>
    /// <param name="account1">��Ҫ�޸ĳɵ��˺���Ϣ</param>
    private void ReviseAccount(string name, Account account)
    {
        this[name] = account;
    }

    /// <summary>
    /// �����˺�
    /// </summary>
    /// <param name="name">��Ҫ���ҵ��˺�����</param>
    /// <returns>���ز鿴�����˺���Ϣ</returns>
    private Account GetAccount(string name)
    {
        return this[name];
    }

    /// <summary>
    /// ��������˺�
    /// </summary>
    /// <returns>���������˺�����</returns>
    private List<Account> GetAllAccount()
    {
        return data.data;
    }

    //--------------------����˺Ų���--------------------//

    /// <summary>
    /// �ж��Ƿ���ڸ����ֵ��˺�
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsName(string key)
    {
        foreach (var item in data.data)
        {
            if (key == item.Name)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �����˺������Ƿ���ȷ
    /// </summary>
    /// <param name="name">��Ҫ�����˺�</param>
    /// <param name="password">��Ҫ��������</param>
    /// <returns>�����Ƿ���ȷ</returns>
    public bool IsPassword(string name,string password)
    {
        if (IsName(name))
        {
            if(GetAccount(name).Password == password)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    //--------------------��װ����--------------------//

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="name">��Ҫ���ҵ��˺�����</param>
    /// <returns>���ز��ҵ��˺�����</returns>
    private Account this[string name]
    {
        get
        {
            foreach (var item in data.data)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return new Account();
        }
        set
        {
            for (int i = 0; i < data.data.Count; i++)
            {
                if(data.data[i].Name == name)
                {
                    data.data[i] = value;
                }
            }
            LoginEventHandler.CallSavaData(data);
        }
    }
}
