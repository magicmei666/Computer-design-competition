using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    private GameObject parentUI;
    private bool allowToggleUI = false;
    public List<ToolbarSlotUI> slotuiList;
    private ToolbarSlotUI selectedSlotUI; //����ǰѡ���
    public Transform Playertransform; // ���������������

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
        InitUI(); // �Ƶ� Awake �����г�ʼ��

        // ȷ������UI��ʼʱ����ʾ
        parentUI.SetActive(false);
        // �������ͨ��Tab�л�����UI����ʾ״̬
        allowToggleUI = true;
    }

    private void Update()
    {
        ToolbarSelectControl();

        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleUI();
        }

        // ȷ���� hoeManager ��صĴ����߼���ȷ���ⲿ���ƺ��ڳ�ͻ�����б�ע�͵��ˣ�
    }
    public ToolbarSlotUI GetselectedSlotUI()
    {
        return selectedSlotUI;
    }
    void InitUI()//9����ݼ�����data���ݺ�UI������Ӧ�ķ���
    {
        slotuiList = new List<ToolbarSlotUI>(new ToolbarSlotUI[9]);
        ToolbarSlotUI[] slotuiArray = transform.GetComponentsInChildren<ToolbarSlotUI>();

        foreach (ToolbarSlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        UpdateUI();//������д��UI���´���
    }

    public void UpdateUI()//��������UI�ķ���
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
