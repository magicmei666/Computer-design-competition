using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform player; // 公共变量，允许你在编辑器中直接指定玩家
    private Vector3 offset;  // 用来存储摄像机与玩家之间的初始偏移量

    void Start()
    {
        // 如果没有在编辑器中设置player，通过代码寻找带有"Player"标签的游戏物体
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // 计算并存储偏移量
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 在每一帧中，更新摄像机的位置，使其平滑地跟随玩家移动
        // 通过添加偏移量确保摄像机保持原来的相对位置
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * 3);
    }
}
