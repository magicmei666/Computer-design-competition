using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }//����ģʽ

    private Dictionary<ItemType, ItemData> itemDataDict = new Dictionary<ItemType, ItemData>();//�ֵ�洢���е���Ʒ��Ϣ

    public InventoryData backpack;//����ֶα��������
    public InventoryData toolbarData;
    public InventoryData JiShi_SuMi;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()//�ֵ��ʼ��
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Data");//�������е�ItenData
        foreach(ItemData data in itemDataArray)
        {
            itemDataDict.Add(data.type, data);
        }

        backpack = Resources.Load<InventoryData>("Data/Backpack");//����ָ����
        toolbarData = Resources.Load<InventoryData>("Data/Toolbar");
        JiShi_SuMi = Resources.Load<InventoryData>("Data/JiShi_SuMi");
    }

    private ItemData GetItemData(ItemType type)//ͨ����Ʒ���ͣ��õ���Ʒ��Ϣ
    {
        ItemData data;
        bool isSuccess = itemDataDict.TryGetValue(type, out data);
        if(isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("�㴫�ݵ�type: " + type + "�����ڣ��޷��õ���Ʒ��Ϣ��");
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

        Debug.LogWarning("�޷�����ֿ⣬��ı���" + backpack + "������");
    }
}
