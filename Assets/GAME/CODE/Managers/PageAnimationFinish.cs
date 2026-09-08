using UnityEngine;

public class PageAnimationFinish : StateMachineBehaviour
{
    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        PageManager pageManager = FindAnyObjectByType<PageManager>();

        if (pageManager != null)
        {
            pageManager.AnimationFinished();
        }
    }
}