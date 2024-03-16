using NodeCanvas.DialogueTrees;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTreeController dialogueController; // �Ի�������������
    public Transform playerTransform; // ���λ������
    public float triggerDistance = 5f; // �����Ի��ľ���
    private bool playerInRange = false; // ����Ƿ��ڷ�Χ��

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // �������Ƿ���NPCһ������֮��
        if (Vector3.Distance(playerTransform.position, transform.position) <= triggerDistance)
        {
            playerInRange = true;
            // ����Ƿ�����"F"��
            if (Input.GetKeyDown(KeyCode.F) && dialogueController != null && !dialogueController.isRunning)
            {
                // ���Կ�ʼ�Ի�
                dialogueController.StartDialogue();
            }
        }
        else
        {
            playerInRange = false;
        }
    }
}


