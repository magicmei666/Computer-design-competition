using System;

/// <summary>
/// �¼�����
/// </summary>
public static class LoginEventHandler
{
    /// <summary>
    /// ������Ϸ���ݵ�����
    /// </summary>
    public static event Action<AccountData> SavaData;
    /// <summary>
    /// ��Ҫ��������ʱ����
    /// </summary>
    /// <param name="data">��Ҫ���汾�ص�����</param>
    public static void CallSavaData(AccountData data)
    {
        SavaData?.Invoke(data);
    }
    /// <summary>
    /// ��ȡ�������ݵ���Ϸ
    /// </summary>
    public static event Func<AccountData> LoadData;
    /// <summary>
    /// ��Ҫ���ر�������ʱ����
    /// </summary>
    /// <returns>���ر��ر������Ϸ����</returns>
    public static AccountData CallLoadData()
    {
        return LoadData?.Invoke();
    }
}
