using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class asdf : StateMachineBehaviour
{
    public AnimationClip animation;
    public string animationClip;
    bool loop;

    [Header("스파인 모션 레이어")]
    public int layer = 0;
    public float timeScale = 1.0f;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private Spine.TrackEntry trackEntry;

    // Start is called before the first frame update
    void Start()
    {
        if (animation != null)
            animationClip = animation.name;
        Debug.Log(animationClip);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        if (skeletonAnimation == null)
        {
            skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            animationState = skeletonAnimation.state;
        }


        if (animationClip != null)
        {
            loop = stateInfo.loop;
            trackEntry = animationState.SetAnimation(layer, animationClip, loop);
            trackEntry.TimeScale = timeScale;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
