using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¶öººµ¥Àý
/// </summary>
public class HungryManSingleton : MonoBehaviour
{
    private static HungryManSingleton Singleton;
    public static HungryManSingleton Instance { get => Singleton; }

    public string csName;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
