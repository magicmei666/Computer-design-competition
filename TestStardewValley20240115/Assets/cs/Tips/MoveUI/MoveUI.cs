using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour,  IDragHandler
{
    private new RectTransform transform;
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.anchoredPosition += eventData.delta;
    }
}
