using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������Ʒ��

public enum ItemType
{
    None,
    Seed_Carrot,
    Seed_Tomato,
    Hoe,
    //Χ��
    fence_left_down,
    fence_right_down,
    fence_left_up,
    fence_right_up,
    fence_hen,
    fence_shu,
    //���ס����ס���ס�����
    a_mi,
    b_mi,
    c_mi,
    d_mi,
}

[CreateAssetMenu()]
public class ItemData : ScriptableObject
{
    public ItemType type = ItemType.None;
    public Sprite sprite;
    public GameObject prefab;
    public int maxCount = 1;
    public bool IsNone()
    {
        return type == ItemType.None;
    }
}
