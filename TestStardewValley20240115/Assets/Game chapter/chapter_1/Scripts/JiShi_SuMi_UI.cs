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
        //transform�����ǶԵ�ǰGameObject���ӵ�Transform��������á���Unity�У�ÿ��GameObject����һ��Transform�����������������λ�á���ת�����š�
        //.Find("ParentUI")������Transform�����һ�����������ڵ�ǰGameObject������ֱ���Ӷ�����������Ϊ"ParentUI"���Ӷ�������ҵ����������ظ��Ӷ����Transform��������û���ҵ�ƥ����Ӷ�����������null��

        //if (parentui == null)
        //{
        //    debug.logerror("parentui not found.");
        //}
        //else
        //{
        //    // ��ʼ��ʱ���ر���ui
        //    parentui.setactive(false);
        //}

    }

    private void Start()
    {
        InitUI(); //д�����֮�󣬳�ʼ���ɹ�

        // ȷ������UI��ʼʱ����ʾ
        parentUI.SetActive(false);
        // �������ͨ��Tab�л�����UI����ʾ״̬
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

    void InitUI()//5����������data���ݺ�UIһһ��Ӧ�ķ���
    {
        JiShiSlotUIList = new List<JiShiSlotUI>(new JiShiSlotUI[5]);
        JiShiSlotUI[] JiShiSlotUIArray = transform.GetComponentsInChildren<JiShiSlotUI>();

        foreach (JiShiSlotUI JiShiSlotUI in JiShiSlotUIArray)
        {
            JiShiSlotUIList[JiShiSlotUI.index] = JiShiSlotUI;
        }

        UpdateUI();//������д��UI���´���
    }

    public void UpdateUI()//��������UI�ķ���
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

    //public void UpdateUI() // ��������UI�ķ���
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
