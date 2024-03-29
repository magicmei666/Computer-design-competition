using System.Collections.Generic;

/// <summary>
/// �˺����� (���������˺ŵ�������Ϣ)
/// </summary>
[System.Serializable]
public class AccountData
{
    /// <summary>
    /// ���е����ݱ������б���
    /// </summary>
    public List<Account> data = new List<Account>();
}
/// <summary>
/// �˺���Ϣ
/// </summary>
[System.Serializable]
public class Account
{
    /// <summary>
    /// �˺�����
    /// </summary>
    public string Name;
    /// <summary>
    /// �˺�����
    /// </summary>
    public string Password;
    /// <summary>
    /// �˺���Ҫ���������
    /// </summary>
    public int Value;
}