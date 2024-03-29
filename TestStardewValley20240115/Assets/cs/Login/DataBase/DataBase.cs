using System.Collections.Generic;
/// <summary>
/// 数据库类 (用于保存与读取本地数据)
/// </summary>
public class DataBase
{
    /// <summary>
    /// 数据库数据
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

    //--------------------增删改查部分(公开部分)--------------------//

    /// <summary>
    /// 添加账号
    /// </summary>
    /// <param name="account">需要添加的账号信息</param>
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
    /// 删除账号
    /// </summary>
    /// <param name="name">需要删除的账号信息</param>
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
    /// 修改账号
    /// </summary>
    /// <param name="account">需要修改的账号</param>
    /// <param name="account1">需要修改成的账号信息</param>
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
    /// 获得所有账号的名字
    /// </summary>
    /// <returns>返回所有账号名字数组</returns>
    public List<string> GetAllAccountName()
    {
        List<string> vs = new List<string>();

        for (int i = 0; i < data.data.Count; i++)
        {
            vs.Add(data.data[i].Name);
        }

        return vs;
    }

    //--------------------增删改查部分(安全部分)--------------------//

    /// <summary>
    /// 添加账号
    /// </summary>
    /// <param name="name">账号名字</param>
    /// <param name="password">账号密码</param>
    /// <returns>返回添加结果</returns>
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
    /// 删除账号
    /// </summary>
    /// <param name="name">需要删除的账号名字</param>
    /// <param name="password">需要删除的账号密码</param>
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
    /// 查找账号
    /// </summary>
    /// <param name="name">需要查找的账号名字</param>
    /// <param name="password">需要查找的账号名字</param>
    /// <returns>返回查找结果</returns>
    public Account GetAccount(string name, string password)
    {
        if (IsPassword(name, password))
        {
            return GetAccount(name);
        }
        return null;
    }

    /// <summary>
    /// 查看该账号的资源
    /// </summary>
    /// <param name="name">需要查看的账号名字</param>
    /// <param name="password">需要查看的密码</param>
    /// <returns>返回账号值</returns>
    public int GetAccountValue(string name)
    {
        if (IsName(name))
        {
            return GetAccount(name).Value;
        }
        return 0;
    }

    //--------------------增删改查部分(私密部分)--------------------//

    /// <summary>
    /// 添加账号
    /// </summary>
    /// <param name="name">账号名字</param>
    /// <param name="password">账号的密码</param>
    /// <param name="value">账号资源</param>
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
    /// 删除账号
    /// </summary>
    /// <param name="name">需要删除的账号名字</param>
    private void Remove(string name)
    {
        data.data.Remove(this[name]);
    }

    /// <summary>
    /// 修改账号
    /// </summary>
    /// <param name="name">需要修改的账号名字</param>
    /// <param name="account1">需要修改成的账号信息</param>
    private void ReviseAccount(string name, Account account)
    {
        this[name] = account;
    }

    /// <summary>
    /// 查找账号
    /// </summary>
    /// <param name="name">需要查找的账号名字</param>
    /// <returns>返回查看到的账号信息</returns>
    private Account GetAccount(string name)
    {
        return this[name];
    }

    /// <summary>
    /// 获得所有账号
    /// </summary>
    /// <returns>返回所有账号数组</returns>
    private List<Account> GetAllAccount()
    {
        return data.data;
    }

    //--------------------检测账号部分--------------------//

    /// <summary>
    /// 判断是否存在该名字的账号
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
    /// 检测该账号密码是否正确
    /// </summary>
    /// <param name="name">需要检测的账号</param>
    /// <param name="password">需要检测的密码</param>
    /// <returns>返回是否正确</returns>
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

    //--------------------封装部分--------------------//

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="name">需要查找的账号名字</param>
    /// <returns>返回查找的账号名字</returns>
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
