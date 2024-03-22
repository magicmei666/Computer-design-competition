using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;

[RequireComponent(typeof(Image))]
public class DialogueUI : MonoBehaviour
{
    public Image speakerImage; // 用于显示说话人头像的UI元素
    public Sprite defaultImage; // 默认头像（如果某个角色没有头像或找不到对应头像）

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

        // 只有当defaultImage不为null时才分配，否则记录一个错误
        if (defaultImage != null)
        {
            speakerImage.sprite = defaultImage;
        }
        else
        {
            Debug.LogError("DefaultImage is not assigned on DialogueUI script.");
        }
        // 对话开始时的逻辑，可以在这里设置默认头像等
        speakerImage.sprite = defaultImage;
    }

    private void OnSubtitlesRequest(SubtitlesRequestInfo info)
    {
        // 更新说话人头像
        var actor = info.actor as IDialogueActor;
        if (actor != null && actor.portraitSprite != null)
        {
            speakerImage.sprite = actor.portraitSprite;
        }
        else
        {
            speakerImage.sprite = defaultImage; // 或者隐藏头像
        }
    }



}
