using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    private GameObject parentUI;
    private bool allowToggleUI = false;
    public List<ToolbarSlotUI> slotuiList;
    private ToolbarSlotUI selectedSlotUI; //代表当前选择的
    public Transform Playertransform; // 保留这个新增属性

    private void Awake()
    {
        parentUI = transform.Find("ParentUI").gameObject;
        if (parentUI == null)
        {
            Debug.LogError("ParentUI not found. Make sure you have set the correct name.");
        }
        //InitUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitUI(); // 移到 Awake 方法中初始化

        // 确保背包UI初始时不显示
        parentUI.SetActive(false);
        // 允许后续通过Tab切换背包UI的显示状态
        allowToggleUI = true;
    }

    private void Update()
    {
        ToolbarSelectControl();

        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleUI();
        }

        // 确保与 hoeManager 相关的代码逻辑正确（这部分似乎在冲突代码中被注释掉了）
    }
    public ToolbarSlotUI GetselectedSlotUI()
    {
        return selectedSlotUI;
    }
    void InitUI()//9个快捷键格子data数据和UI各个对应的方法
    {
        slotuiList = new List<ToolbarSlotUI>(new ToolbarSlotUI[9]);
        ToolbarSlotUI[] slotuiArray = transform.GetComponentsInChildren<ToolbarSlotUI>();

        foreach (ToolbarSlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        UpdateUI();//这里少写了UI导致错误
    }

    public void UpdateUI()//更新所有UI的方法
    {
        List<SlotData> slotdataList = InventoryManager.Instance.toolbarData.slotList;

        for (int i = 0; i < slotdataList.Count; i++)
        {
            slotuiList[i].SetData(slotdataList[i]);
        }
    }
    void ToolbarSelectControl()
    {
        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {

                if (selectedSlotUI != null)
                {
                    selectedSlotUI.UnHighlight();
                }
                int index = i - (int)KeyCode.Alpha1;
                if (!slotuiList[index].GetData().IsEmpty())
                {
                    selectedSlotUI = slotuiList[index];

                    selectedSlotUI.Highlight();
                }
            }
        }
    }

    private void ToggleUI()
    {
        if (parentUI != null)
        {
            if (allowToggleUI)
            {
                parentUI.SetActive(!parentUI.activeSelf);
            }
        }
        else
        {
            Debug.LogError("Attempted to toggle UI but parentUI is not set.");
        }
    }

}
