using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform player; // �����������������ڱ༭����ֱ��ָ�����
    private Vector3 offset;  // �����洢����������֮��ĳ�ʼƫ����

    void Start()
    {
        // ���û���ڱ༭��������player��ͨ������Ѱ�Ҵ���"Player"��ǩ����Ϸ����
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // ���㲢�洢ƫ����
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // ��ÿһ֡�У������������λ�ã�ʹ��ƽ���ظ�������ƶ�
        // ͨ�����ƫ����ȷ�����������ԭ�������λ��
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * 3);
    }
}
