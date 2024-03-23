using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public ItemType type;
    public bool isFence() {
        return type >= ItemType.fence_left_down && type <= ItemType.fence_shu;
    }
    public bool can_pick()
    {
        if (isFence())
        {
            if (Input.GetKey(KeyCode.LeftShift)) { 
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
