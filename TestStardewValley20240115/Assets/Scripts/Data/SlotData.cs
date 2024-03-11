using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData 
{
    public ItemData item;
    public int count = 0;

    private Action OnChange;//关于更新的事件

    public void MoveSlot(SlotData data)
    {
        this.item = data.item;
        this.count = data.count;
        OnChange?.Invoke();
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public bool CanAddItem()
    {
        return count < item.maxCount;
    }
    public int GetFreeSpace()
    {
        return item.maxCount - count;
    }
    public void Add(int numToAdd = 1)
    {
        this.count += numToAdd;
        OnChange?.Invoke();//OnChange不为空就调用这个方法
    }
    public void AddItem(ItemData item,int count =1)
    {
        this.item = item;
        this.count = count;
        OnChange?.Invoke();
    }
    public void Reduce(int numToReduce = 1)
    {
        count -= numToReduce;
        if (count == 0)
        {
            Clear();
        }
        else
        {
            OnChange?.Invoke();
        }
    }
    public void Clear()
    {
        item = null;
        count = 0;
        OnChange?.Invoke();
    }

    public void AddListener(Action OnChange)//添加事件
    {
        this.OnChange = OnChange;
    }
}
