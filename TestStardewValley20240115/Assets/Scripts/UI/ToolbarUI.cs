using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public GameObject parentUI;
    public List<ToolbarSlotUI> slotuiList;
    private ToolbarSlotUI selectedSlotUI;//代表当前选择的
    public Transform Playertransform;
    // Start is called before the first frame update
    void Start()
    {
        parentUI = transform.Find("ParentUI").gameObject;
        InitUI();
    }
    private void Update()
    {
        ToolbarSelectControl();

        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleUI();
        }


        //if (selectedSlotUI != null
        //    && selectedSlotUI.GetData().item.type == ItemType.Hoe
        //    && Input.GetKeyDown(KeyCode.Space))
        //{
        //    hoeManager.Instance.Uptohoe(Playertransform.position);

        //}
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
            parentUI.SetActive(!parentUI.activeSelf);
        }
        else
        {
            Debug.LogError("Attempted to toggle UI but parentUI is not set.");
        }
    }

}
