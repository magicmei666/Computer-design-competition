using UnityEngine;
using UnityEngine.SceneManagement;

public class ZhuanHuan_ShiJi : MonoBehaviour
{
    public string sceneNameToLoad; // 要加载的场景名称
    public Transform destination; // 特定地点的位置
    public float triggerDistance = 7.0f; // 触发距离

    private Transform playerTransform; // 玩家位置

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; // 找到玩家的Transform
    }

    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, destination.position) <= triggerDistance)
        {
            // 玩家在特定地点的触发距离内
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
            {
                // 加载指定的场景
                SceneManager.LoadSceneAsync(sceneNameToLoad);
            }
        }
    }
}


