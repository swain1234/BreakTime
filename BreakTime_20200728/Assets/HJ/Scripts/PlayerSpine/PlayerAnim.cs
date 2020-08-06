using System.Collections;
using UnityEngine;
using Spine.Unity;

public class PlayerAnim : StateMachineBehaviour
{
    public AnimationClip animation;
    // 현재 실행 중인 애니메이션
    public string currentAnimation;

    [Header("스파인 모션 레이어")]
    public int layer = 0;
    public float timeScale = 1.0f;
    bool isLoop;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private Spine.TrackEntry trackEntry;

    void Start()
    {
        if(animation != null)
            currentAnimation = animation.name;
        Debug.Log(currentAnimation);
        
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            animationState = skeletonAnimation.state;
        }

        if(currentAnimation != null)
        {
            isLoop = stateInfo.loop;
            trackEntry = animationState.SetAnimation(layer, currentAnimation, isLoop);
            trackEntry.TimeScale = timeScale;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
