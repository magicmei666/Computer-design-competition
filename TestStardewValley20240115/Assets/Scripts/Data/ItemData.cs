using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是物品类

public enum ItemType
{
    None,
    Seed_Carrot,
    Seed_Tomato,
    Hoe,
    //围栏
    fence_left_down,
    fence_right_down,
    fence_left_up,
    fence_right_up,
    fence_hen,
    fence_shu,
    //粝米、稗米、绺米、御米
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
