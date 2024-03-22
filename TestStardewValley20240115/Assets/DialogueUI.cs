using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;

[RequireComponent(typeof(Image))]
public class DialogueUI : MonoBehaviour
{
    public Image speakerImage; // ������ʾ˵����ͷ���UIԪ��
    public Sprite defaultImage; // Ĭ��ͷ�����ĳ����ɫû��ͷ����Ҳ�����Ӧͷ��

    private void Awake()
    {
        DialogueTree.OnDialogueStarted += OnDialogueStarted;
        DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;

        speakerImage = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        DialogueTree.OnDialogueStarted -= OnDialogueStarted;
        DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
    }

    private void OnDialogueStarted(DialogueTree dlgTree)
    {
        if (speakerImage == null)
        {
            Debug.LogError("SpeakerImage is not assigned on DialogueUI script.");
            return;
        }

        // ֻ�е�defaultImage��Ϊnullʱ�ŷ��䣬�����¼һ������
        if (defaultImage != null)
        {
            speakerImage.sprite = defaultImage;
        }
        else
        {
            Debug.LogError("DefaultImage is not assigned on DialogueUI script.");
        }
        // �Ի���ʼʱ���߼�����������������Ĭ��ͷ���
        speakerImage.sprite = defaultImage;
    }

    private void OnSubtitlesRequest(SubtitlesRequestInfo info)
    {
        // ����˵����ͷ��
        var actor = info.actor as IDialogueActor;
        if (actor != null && actor.portraitSprite != null)
        {
            speakerImage.sprite = actor.portraitSprite;
        }
        else
        {
            speakerImage.sprite = defaultImage; // ��������ͷ��
        }
    }



}
