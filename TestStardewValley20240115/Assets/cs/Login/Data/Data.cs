using System.Collections.Generic;

/// <summary>
/// 账号数据 (保存所有账号的数据信息)
/// </summary>
[System.Serializable]
public class AccountData
{
    /// <summary>
    /// 所有的数据保存在列表里
    /// </summary>
    public List<Account> data = new List<Account>();
}
/// <summary>
/// 账号信息
/// </summary>
[System.Serializable]
public class Account
{
    /// <summary>
    /// 账号名字
    /// </summary>
    public string Name;
    /// <summary>
    /// 账号密码
    /// </summary>
    public string Password;
    /// <summary>
    /// 账号需要保存的数据
    /// </summary>
    public int Value;
}