using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class StartToWoShi : MonoBehaviour
{
    // 创建一个公开的方法来加载场景，通过场景的名字来指定加载哪个场景
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
