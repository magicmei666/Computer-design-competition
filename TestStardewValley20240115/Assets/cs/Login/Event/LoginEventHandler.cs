using System;

/// <summary>
/// 事件中心
/// </summary>
public static class LoginEventHandler
{
    /// <summary>
    /// 保存游戏数据到本地
    /// </summary>
    public static event Action<AccountData> SavaData;
    /// <summary>
    /// 需要保存数据时调用
    /// </summary>
    /// <param name="data">需要保存本地的数据</param>
    public static void CallSavaData(AccountData data)
    {
        SavaData?.Invoke(data);
    }
    /// <summary>
    /// 读取本地数据到游戏
    /// </summary>
    public static event Func<AccountData> LoadData;
    /// <summary>
    /// 需要加载本地数据时调用
    /// </summary>
    /// <returns>返回本地保存的游戏数据</returns>
    public static AccountData CallLoadData()
    {
        return LoadData?.Invoke();
    }
}
