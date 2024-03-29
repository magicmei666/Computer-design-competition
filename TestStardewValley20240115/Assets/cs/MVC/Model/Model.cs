using System;

/// <summary>
/// ����ģ��
/// </summary>
public abstract class Model
{
    //��ʼ������
    public abstract void Init();

    //����ע����� ����֪ͨ����View��ȥ��������
    private event Action<Model> updateEvent;
    //ע���¼� һ��ע��͸���
    public void AddUpdateEvent(Action<Model> action)
    {
        updateEvent += action;
        CallUpdateEvent();
    }
    //֪ͨView�����UI
    public void CallUpdateEvent()
    {
        updateEvent?.Invoke(this);
    }

}

/// <summary>
/// ��������ģ�Ͳ�
/// </summary>
/// <typeparam name="T">�̳�Model�������</typeparam>
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
