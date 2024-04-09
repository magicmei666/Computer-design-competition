using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiShi_SuMi_UI : MonoBehaviour
{
    private GameObject parentUI;

    private bool allowToggleUI = false;

    public List<JiShiSlotUI> JiShiSlotUIList;

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

        // 确保背包UI初始时不显示
        parentUI.SetActive(false);
        // 允许后续通过Tab切换背包UI的显示状态
        allowToggleUI = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleUI();
        }
    }

    void InitUI()//5个背包格子data数据和UI一一对应的方法
    {
        JiShiSlotUIList = new List<JiShiSlotUI>(new JiShiSlotUI[5]);
        JiShiSlotUI[] JiShiSlotUIArray = transform.GetComponentsInChildren<JiShiSlotUI>();

        foreach (JiShiSlotUI JiShiSlotUI in JiShiSlotUIArray)
        {
            JiShiSlotUIList[JiShiSlotUI.index] = JiShiSlotUI;
        }

        UpdateUI();//这里少写了UI导致错误
    }

    public void UpdateUI()//更新所有UI的方法
    {
        List<SlotData> slotdataList = InventoryManager.Instance.backpack.slotList;

        for (int i = 0; i < slotdataList.Count; i++)
        {
            JiShiSlotUIList[i].SetData(slotdataList[i]);
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
            if (i < JiShiSlotUIList.Count && JiShiSlotUIList[i] != null)
            {
                JiShiSlotUIList[i].SetData(slotdataList[i]);
            }
            else
            {
                Debug.LogError("JiShiSlotUIList is not properly initialized or out of index.");
            }
        }
    }

    //public void UpdateUI() // 更新所有UI的方法
    //{
    //    List<SlotData> slotdataList = InventoryManager.Instance.backpack.slotList;
    //    int count = Math.Min(slotdataList.Count, JiShiSlotUIList.Count);

    //    for (int i = 0; i < count; i++)
    //    {
    //        JiShiSlotUIList[i].SetData(slotdataList[i]);
    //    }
    //}


    private void ToggleUI()
    {
        if (allowToggleUI)
        {
            parentUI.SetActive(!parentUI.activeSelf);
        }
    }

    public void OnCloseClick()
    {
        ToggleUI();
    }
}
