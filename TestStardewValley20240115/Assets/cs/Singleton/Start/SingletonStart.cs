using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonStart : MonoBehaviour
{
    private void Start()
    {
        StartHungryManSingleton();
        StartSlackerSingleton();
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void StartHungryManSingleton()
    {
        Debug.Log(HungryManSingleton.Instance.csName);
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void StartSlackerSingleton()
    {
        Debug.Log(SlackerSingleton.Instance.csName);
    }
}
