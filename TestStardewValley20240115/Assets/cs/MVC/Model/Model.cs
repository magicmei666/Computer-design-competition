using System;

/// <summary>
/// 数据模型
/// </summary>
public abstract class Model
{
    //初始化方法
    public abstract void Init();

    //建立注册机制 用于通知所有View层去更新数据
    private event Action<Model> updateEvent;
    //注册事件 一旦注册就更新
    public void AddUpdateEvent(Action<Model> action)
    {
        updateEvent += action;
        CallUpdateEvent();
    }
    //通知View层更新UI
    public void CallUpdateEvent()
    {
        updateEvent?.Invoke(this);
    }

}

/// <summary>
/// 泛型数据模型层
/// </summary>
/// <typeparam name="T">继承Model本身的类</typeparam>
public abstract class Model<T> : Model where  T : Model<T> ,new()
{
    private static T model;
    public static T Instance
    {
        get
        {
            if (model == null)
            {
                model = new T();
                model.Init();
            }
            return model;
        }
    }
}
