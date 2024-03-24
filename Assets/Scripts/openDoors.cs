using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoors : MonoBehaviour
{
    
    public AnimationCurve positionCurveY;
    public float doorSpeed = 1.0f;
    public float cyclesToWait = 0.0f;
        
    private BoxCollider2D boxCollider;
    private Animator animator;
    private AnimationClip[] animationClips;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        

        // Ensure that curves are not null
        if ( positionCurveY == null)
        {
            Debug.LogError("One or more Animation Curves are not assigned.");
            return;
        }

        // Get the original size and offset of the collider
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;

        // Get all animation clips from the Animator
        animationClips = animator.runtimeAnimatorController.animationClips;

        // Subscribe to animation events
        foreach (AnimationClip clip in animationClips)
        {
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = clip.length; // Set event time to end of animation
            animEvent.functionName = "ResetCollider"; // Function to call at end of animation
            clip.AddEvent(animEvent);
        }
    }

    void Update()
    {
        if(GameMaster.Instance.timerModified <= cyclesToWait / doorSpeed * 0.667f)
        {
            animator.speed = 0;
            return;
        }
        // Get the current animation state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.speed = doorSpeed * GameMaster.Instance.timeMultiplayer;

        // Evaluate animation curves and update collider size and position
        float t = stateInfo.normalizedTime % 1.0f;
        //Vector2 size = new Vector2(originalSize.x, sizeCurveY.Evaluate(t));
        Vector2 position = new Vector2(originalOffset.x, originalOffset.y + positionCurveY.Evaluate(t));
        //boxCollider.size = size;
        boxCollider.offset = position;
    }

    // This function resets the collider after the animation ends
    void ResetCollider()
    {
        boxCollider.size = originalSize; // Reset collider size to default
        boxCollider.offset = originalOffset; // Reset collider position to default
    }
}
