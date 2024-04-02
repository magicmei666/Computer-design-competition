using UnityEngine;
using UnityEngine.UI; // 引用UI命名空间

public class DialogueInputManager : MonoBehaviour
{
    public GameObject dialogueUI; // 对话UI界面
    public InputField inputField; // 玩家输入字段
    public Button submitButton; // 提交按钮
    public Text feedbackText; // 反馈信息文本

    void Start()
    {
        submitButton.onClick.AddListener(ValidateInput);
        dialogueUI.SetActive(false); // 初始时隐藏对话UI
    }

    // 调用这个方法来显示对话UI界面
    public void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
    }

    // 验证玩家的输入
    void ValidateInput()
    {
        if (inputField.text == "正确答案") // 假设"正确答案"是期望的输入
        {
            // 玩家输入正确，继续后续对话或游戏逻辑
            feedbackText.text = "正确！";
            dialogueUI.SetActive(false); // 或者进行到下一个对话
        }
        else
        {
            // 玩家输入错误，显示反馈
            feedbackText.text = "错误，请重试。";
        }
    }
}
