using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }//单例模式

    private Dictionary<ItemType, ItemData> itemDataDict = new Dictionary<ItemType, ItemData>();//字典存储所有的物品信息

    public InventoryData backpack;//这个字段保存的数据
    public InventoryData toolbarData;
    public InventoryData JiShi_SuMi;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()//字典初始化
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Data");//加载所有的ItenData
        foreach(ItemData data in itemDataArray)
        {
            itemDataDict.Add(data.type, data);
        }

        backpack = Resources.Load<InventoryData>("Data/Backpack");//加载指定的
        toolbarData = Resources.Load<InventoryData>("Data/Toolbar");
        JiShi_SuMi = Resources.Load<InventoryData>("Data/JiShi_SuMi");
    }

    private ItemData GetItemData(ItemType type)//通过物品类型，得到物品信息
    {
        ItemData data;
        bool isSuccess = itemDataDict.TryGetValue(type, out data);
        if(isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("你传递的type: " + type + "不存在，无法得到物品信息。");
            return null;
        }
    }

    public void AddToBackpack(ItemType type)
    {
        ItemData item = GetItemData(type);
        if (item == null) return;

        foreach(SlotData slotData in backpack.slotList)
        {
            if(slotData.item == item && slotData.CanAddItem())
            {
                slotData.Add();
                return;
            }
        }

        foreach (SlotData slotData in backpack.slotList)
        {
            if(slotData.item==null && slotData.count==0)
            {
                slotData.AddItem(item);
                return;
            }
        }

        Debug.LogWarning("无法放入仓库，你的背包" + backpack + "已满。");
    }
}
