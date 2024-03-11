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
        //transform�����ǶԵ�ǰGameObject���ӵ�Transform��������á���Unity�У�ÿ��GameObject����һ��Transform������������������λ�á���ת�����š�
        //.Find("ParentUI")������Transform�����һ�����������ڵ�ǰGameObject������ֱ���Ӷ�����������Ϊ"ParentUI"���Ӷ�������ҵ����������ظ��Ӷ����Transform��������û���ҵ�ƥ����Ӷ�����������null��
    }

    private void Start()
    {
        InitUI(); //д�����֮�󣬳�ʼ���ɹ�
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab ))
        {
            ToggleUI();
        }
    }

    void InitUI()//24����������data���ݺ�UIһһ��Ӧ�ķ���
    {
        slotuiList = new List<SlotUI>(new SlotUI[24]);
        SlotUI[] slotuiArray = transform.GetComponentsInChildren<SlotUI>();

        foreach (SlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        UpdateUI();//������д��UI���´���
    }

    public void UpdateUI()//��������UI�ķ���
    {
        List<SlotData> slotdataList = InventoryManager.Instance.backpack.slotList;

        for (int i = 0; i < slotdataList.Count; i++) 
        {
            slotuiList[i].SetData(slotdataList[i]);
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