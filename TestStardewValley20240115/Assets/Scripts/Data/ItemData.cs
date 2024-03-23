using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Seed_Carrot,
    Seed_Tomato,
    Hoe,
    fence_left_down,
    fence_right_down,
    fence_left_up,
    fence_right_up,
    fence_hen,
    fence_shu
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
