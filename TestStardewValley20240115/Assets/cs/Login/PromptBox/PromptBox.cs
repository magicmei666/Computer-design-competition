using UnityEngine;
using TMPro;

/// <summary>
/// 对话框类
/// </summary>
public class PromptBox : MonoBehaviour
{
    //显示框显示的文本
    private TMP_Text text;

    //获得场景中对象的组件给变量赋值
    private void Awake()
    {
        text = transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
    }
    //显示对话框以及显示文字
    public void ShowBox(string showText)
    {
        this.gameObject.SetActive(true);
        text.text = showText;
    }
    //动画结束时调用的函数
    public void End()
    {
        this.gameObject.SetActive(false);
    }
}
