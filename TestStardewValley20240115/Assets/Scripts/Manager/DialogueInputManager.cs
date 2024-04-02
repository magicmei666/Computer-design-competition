using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class DialogueInputManager : MonoBehaviour
{
    public GameObject dialogueUI; // �Ի�UI����
    public InputField inputField; // ��������ֶ�
    public Button submitButton; // �ύ��ť
    public Text feedbackText; // ������Ϣ�ı�

    void Start()
    {
        submitButton.onClick.AddListener(ValidateInput);
        dialogueUI.SetActive(false); // ��ʼʱ���ضԻ�UI
    }

    // ���������������ʾ�Ի�UI����
    public void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
    }

    // ��֤��ҵ�����
    void ValidateInput()
    {
        if (inputField.text == "��ȷ��") // ����"��ȷ��"������������
        {
            // ���������ȷ�����������Ի�����Ϸ�߼�
            feedbackText.text = "��ȷ��";
            dialogueUI.SetActive(false); // ���߽��е���һ���Ի�
        }
        else
        {
            // ������������ʾ����
            feedbackText.text = "���������ԡ�";
        }
    }
}
