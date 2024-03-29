using UnityEngine;
using TMPro;

/// <summary>
/// �Ի�����
/// </summary>
public class PromptBox : MonoBehaviour
{
    //��ʾ����ʾ���ı�
    private TMP_Text text;

    //��ó����ж���������������ֵ
    private void Awake()
    {
        text = transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
    }
    //��ʾ�Ի����Լ���ʾ����
    public void ShowBox(string showText)
    {
        this.gameObject.SetActive(true);
        text.text = showText;
    }
    //��������ʱ���õĺ���
    public void End()
    {
        this.gameObject.SetActive(false);
    }
}
