using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;
using TMPro; // 确保导入TextMeshPro命名空间

public class DialogueInputController : MonoBehaviour
{
    public GameObject inputUI; // 输入UI的容器
    public List<TMP_InputField> inputFields = new List<TMP_InputField>(); // 所有的输入字段
    public Button confirmButton; // 确认按钮
    public DialogueTreeController correctDialogueTree; // 玩家输入正确时的对话树
    public DialogueTreeController incorrectDialogueTree; // 玩家输入错误时的对话树
    //public DialogueTreeController simpleDialogueTree;
    private List<string> correctAnswers = new List<string> { "12", "34","56", "78","91","100" }; // 正确答案列表

    void Awake()
    {
        confirmButton.onClick.AddListener(CheckInputsAndContinueDialogue);
        inputUI.SetActive(false); // 默认隐藏输入UI
    }

    void OnEnable()
    {
        // 监听自定义事件，以显示输入UI
        //DialogueTree.OnDialogueStarted += OnDialogueStarted;
    }

    void OnDisable()
    {
        // 停止监听事件
        //DialogueTree.OnDialogueStarted -= OnDialogueStarted;
    }

    //private void OnDialogueStarted(DialogueTree obj)
    //{
    //    // 检查是否是我们想要响应的对话树
    //    if (obj == correctDialogueTree || obj == incorrectDialogueTree)
    //    {
    //        ShowInputUI();
    //    }
    //}

    public void ShowInputUI()
    {
        inputUI.SetActive(true); // 显示输入UI
    }

    public void HuiDaoDiTu()
    {
        SceneManager.LoadSceneAsync("1_JiShiDiTu_After");
    }

    private void CheckInputsAndContinueDialogue()
    {
        bool allCorrect = true;
        for (int i = 0; i < inputFields.Count; i++)
        {
            if (inputFields[i].text.Trim() != correctAnswers[i])
            {
                allCorrect = false;
                break;
            }
        }

        inputUI.SetActive(false); // 隐藏输入UI

        if (allCorrect)
        {
            correctDialogueTree.StartDialogue(); // 开始正确答案的对话树
        }
        else
        {
            incorrectDialogueTree.StartDialogue(); // 开始错误答案的对话树
        }
    }
}