using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //��¼��ҿ�ʼ��קʱ��λ��
    private Vector3 vector;
    //��Ҫ�ƶ���Ʒ��λ�����
    private RectTransform rectTransform;
    //UI�¼�������
    private EventSystem _EventSystem;
    private GraphicRaycaster gra;

    private void Awake()
    {
        _EventSystem = FindObjectOfType<EventSystem>();
        gra = FindObjectOfType<GraphicRaycaster>();

        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ��ʼ��קʱִ��һ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        vector = this.transform.position;
    }

    /// <summary>
    /// ��קʱ��������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta;
    }

    /// <summary>
    /// ������קʱִ��һ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //�ж��Ƿ���ק��������
        bool isSolt = false;
        //������ӵ�λ��
        Vector3 SlotVector = new Vector3();
        //ͨ������Ķ�ȡ������ȡ�������λ�õ�UI����
        var list = GraphicRaycaster(Input.mousePosition);
        foreach (var item in list)
        {
            //����Ƿ�����Ʒ��
            if (item.gameObject.tag == "Item")
            {
                //�������Ʒ����ִ�����´���
                //---����λ��---//
                this.rectTransform.position = item.gameObject.transform.position;
                item.gameObject.transform.position = vector;
            }
            //����Ƿ��ڸ�����
            else if(item.gameObject.tag == "Slot")
            {
                //����ڸ�������isSlot����Ϊtrue�����������
                isSolt = true;
                //�������λ�������Ա��л�λ��
                SlotVector = item.gameObject.transform.position;
            }
        }
        //�ж��Ƿ����ڸ�����
        if (isSolt)
        {
            //����ڸ�����,���л���������
            this.rectTransform.position = SlotVector;
        }
        else
        {
            //������ڸ�����,�򷵻�ԭλ��
            this.rectTransform.position = vector;
        }
    }

    /// <summary>
    /// ����ͨ�����߶�ȡ����λ�õ�UI����
    /// </summary>
    /// <param name="pos">����λ��</param>
    /// <returns>���ض�ȡ������UI����</returns>
    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {
        var mPointerEventData = new PointerEventData(_EventSystem);
        mPointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        gra.Raycast(mPointerEventData, results);
        return results;
    }

}
