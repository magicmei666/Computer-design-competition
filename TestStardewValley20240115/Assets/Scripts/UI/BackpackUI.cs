using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    private GameObject parentUI;

    public List<SlotUI> slotuiList;

    private void Awake()
    {
        parentUI = transform.Find("ParentUI").gameObject;
        //transform：这是对当前GameObject附加的Transform组件的引用。在Unity中，每个GameObject都有一个Transform组件，它负责处理对象的位置、旋转和缩放。
        //.Find("ParentUI")：这是Transform组件的一个方法，它在当前GameObject的所有直接子对象中搜索名为"ParentUI"的子对象。如果找到，它将返回该子对象的Transform组件。如果没有找到匹配的子对象，它将返回null。

        //if (parentui == null)
        //{
        //    debug.logerror("parentui not found.");
        //}
        //else
        //{
        //    // 初始化时隐藏背包ui
        //    parentui.setactive(false);
        //}

    }

    private void Start()
    {
        InitUI(); //写了这个之后，初始化成功
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab ))
        {
            ToggleUI();
        }
    }

    void InitUI()//24个背包格子data数据和UI一一对应的方法
    {
        slotuiList = new List<SlotUI>(new SlotUI[24]);
        SlotUI[] slotuiArray = transform.GetComponentsInChildren<SlotUI>();

        foreach (SlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        UpdateUI();//这里少写了UI导致错误
    }

    public void UpdateUI()//更新所有UI的方法
    {
        List<SlotData> slotdataList = InventoryManager.Instance.backpack.slotList;

        for (int i = 0; i < slotdataList.Count; i++) 
        {
            slotuiList[i].SetData(slotdataList[i]);
        }

        if (InventoryManager.Instance == null || InventoryManager.Instance.backpack == null)
        {
            Debug.LogError("InventoryManager or backpack is not initialized.");
            return;
        }

        _ = InventoryManager.Instance.backpack.slotList;
        if (slotdataList == null)
        {
            Debug.LogError("slotdataList is null.");
            return;
        }

        for (int i = 0; i < slotdataList.Count; i++)
        {
            if (i < slotuiList.Count && slotuiList[i] != null)
            {
                slotuiList[i].SetData(slotdataList[i]);
            }
            else
            {
                Debug.LogError("slotuiList is not properly initialized or out of index.");
            }
        }
    }

    private void ToggleUI()
    {
        parentUI.SetActive(!parentUI.activeSelf);
    }

    public void OnCloseClick()
    {
        ToggleUI();
    }
}
