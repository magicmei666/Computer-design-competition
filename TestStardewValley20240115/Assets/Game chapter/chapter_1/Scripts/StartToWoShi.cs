using UnityEngine;
using UnityEngine.SceneManagement; // ���볡�����������ռ�

public class StartToWoShi : MonoBehaviour
{
    // ����һ�������ķ��������س�����ͨ��������������ָ�������ĸ�����
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
