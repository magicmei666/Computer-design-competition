using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÀÁººµ¥Àý
/// </summary>
public class SlackerSingleton : MonoBehaviour
{
    private static SlackerSingleton Singleton;
    public static SlackerSingleton Instance
    {
        get
        {
            if (Singleton != null)
                return Singleton;

            Singleton = FindObjectOfType<SlackerSingleton>();

            if(Singleton == null)
            {
                Singleton = new SlackerSingleton();
            }
            else
            {
                foreach (var item in FindObjectsOfType<SlackerSingleton>())
                {
                    if (item != Singleton)
                        Destroy(item.gameObject);
                }
            }
            return Singleton;
        }
    }

    public string csName;
}
