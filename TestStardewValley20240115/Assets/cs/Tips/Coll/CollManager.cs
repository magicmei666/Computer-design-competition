using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollManager : MonoBehaviour
{
    public List<cs> coll;
    private void Start()
    {
        foreach (var item in coll)
        {
            Physics2D.IgnoreCollision(item.collider1, item.collider2);
        }

    }
}
[System.Serializable]
public class cs
{
    public Collider2D collider1, collider2;
}
