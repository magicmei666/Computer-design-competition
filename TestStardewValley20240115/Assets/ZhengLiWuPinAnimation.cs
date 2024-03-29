using UnityEngine;

public class PlayAnimation1 : MonoBehaviour
{
    public Animator npcAnimator;
    public Animator playerAnimator;
    public string npcAnimationTrigger;
    public string playerAnimationTrigger;

    public void TriggerAnimations()
    {
        if (npcAnimator && npcAnimationTrigger != "")
        {
            npcAnimator.SetTrigger(npcAnimationTrigger);
        }
        if (playerAnimator && playerAnimationTrigger != "")
        {
            playerAnimator.SetTrigger(playerAnimationTrigger);
        }
    }
}
