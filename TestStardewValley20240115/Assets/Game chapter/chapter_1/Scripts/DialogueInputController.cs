using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;
using TMPro; // ȷ������TextMeshPro�����ռ�

public class DialogueInputController : MonoBehaviour
{
    public GameObject inputUI; // ����UI������
    public List<TMP_InputField> inputFields = new List<TMP_InputField>(); // ���е������ֶ�
    public Button confirmButton; // ȷ�ϰ�ť
    public DialogueTreeController correctDialogueTree; // ���������ȷʱ�ĶԻ���
    public DialogueTreeController incorrectDialogueTree; // ����������ʱ�ĶԻ���
    //public DialogueTreeController simpleDialogueTree;
    private List<string> correctAnswers = new List<string> { "12", "34","56", "78","91","100" }; // ��ȷ���б�

    void Awake()
    {
        confirmButton.onClick.AddListener(CheckInputsAndContinueDialogue);
        inputUI.SetActive(false); // Ĭ����������UI
    }

    void OnEnable()
    {
        // �����Զ����¼�������ʾ����UI
        //DialogueTree.OnDialogueStarted += OnDialogueStarted;
    }

    void OnDisable()
    {
        // ֹͣ�����¼�
        //DialogueTree.OnDialogueStarted -= OnDialogueStarted;
    }

    //private void OnDialogueStarted(DialogueTree obj)
    //{
    //    // ����Ƿ���������Ҫ��Ӧ�ĶԻ���
    //    if (obj == correctDialogueTree || obj == incorrectDialogueTree)
    //    {
    //        ShowInputUI();
    //    }
    //}

    public void ShowInputUI()
    {
        inputUI.SetActive(true); // ��ʾ����UI
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

        inputUI.SetActive(false); // ��������UI

        if (allCorrect)
        {
            correctDialogueTree.StartDialogue(); // ��ʼ��ȷ�𰸵ĶԻ���
        }
        else
        {
            incorrectDialogueTree.StartDialogue(); // ��ʼ����𰸵ĶԻ���
        }
    }
}