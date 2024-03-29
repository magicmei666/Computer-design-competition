using NodeCanvas.DialogueTrees;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTreeController dialogueController; // 对话树控制器引用
    public Transform playerTransform; // 玩家位置引用
    public float triggerDistance = 5f; // 触发对话的距离
    private bool playerInRange = false; // 玩家是否在范围内

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // 检测玩家是否在NPC一定距离之内
        if (Vector3.Distance(playerTransform.position, transform.position) <= triggerDistance)
        {
            playerInRange = true;
            // 检测是否按下了"F"键
            if (Input.GetKeyDown(KeyCode.F) && dialogueController != null && !dialogueController.isRunning)
            {
                // 尝试开始对话
                dialogueController.StartDialogue();
            }
        }
        else
        {
            playerInRange = false;
        }
    }
}


