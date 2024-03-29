using UnityEngine;
using UnityEngine.SceneManagement;

public class ChuanSongSimpleScene : MonoBehaviour
{
    public string sceneNameToLoad; // Ҫ���صĳ�������
    public Transform destination; // �ض��ص��λ��
    public float triggerDistance = 3.0f; // ��������

    private Transform playerTransform; // ���λ��

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; // �ҵ���ҵ�Transform
    }

    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, destination.position) <= triggerDistance)
        {
            // ������ض��ص�Ĵ���������
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
            {
                // ����ָ���ĳ���
                SceneManager.LoadSceneAsync("SampleScene");
            }
        }
    }
}


