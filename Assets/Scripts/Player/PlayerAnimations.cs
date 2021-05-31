using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation anim;
    [SerializeField] private AnimationStates states;
    [SerializeField] private string currentAnimation;
    public enum BayoStates { idle, walking, pushWalk, jump, pickUp, climb, ball };
    [SerializeField] private BayoStates currentState;

    [SerializeField] private float raycastDistance;
    //private SpriteRenderer spriteRenderer;

    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerGrab grab;
    public bool playGrab = false;
    //[SerializeField] private Sprite grabSprite;

    private PlayerLedgeClimb climb;
    //[SerializeField] private Sprite climbSprite;

    private PlayerBall ball;
    //[SerializeField] private Sprite ballSprite, normalSprite;

    public Transform pivot;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        grab = GetComponent<PlayerGrab>();
        climb = GetComponent<PlayerLedgeClimb>();
        ball = GetComponent<PlayerBall>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentState = BayoStates.idle;
        SetCharacterState(currentState);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rotate pivot to direction
        if (movement.direction != 0 && !climb.isClimbing && !ball.isBall)
        {
            float stickRaw = movement.direction;
            float newDir;
            if (stickRaw > 0 && stickRaw <= 1) newDir = 1;
            else if (stickRaw < 0 && stickRaw >= -1) newDir = -1;
            else newDir = 0;
            pivot.localScale = new Vector3(newDir, pivot.localScale.y, pivot.localScale.z);
        }

        //Change rotation of pivot of normal from ground
        if (jump.IsGrounded() && !ball.isBall && !climb.isClimbing)
        {
            Debug.DrawRay(grab.grabPoint.position, Vector2.down * raycastDistance, Color.red);
            pivot.rotation = GetNormalFromGround();
        }

        //Set walking animation
        if (movement.movementInput != 0 && !ball.isBall && !climb.isClimbing && !jump.inJump && !movement.isPushing && !playGrab) { SetCharacterState(BayoStates.walking); playGrab = false; }

        else if (playGrab)
        {
            SetCharacterState(BayoStates.pickUp);
            Invoke("ResetGrab", .3f);
        }

        //Set ball animation
        else if (ball.isBall)
        {
            SetCharacterState(BayoStates.ball);
        }

        //Set climb animation
        else if (climb.isClimbing)
        {
            SetCharacterState(BayoStates.climb);
        }

        else if (jump.inJump)
        {
            SetCharacterState(BayoStates.jump);
        }

        else if (movement.isPushing && movement.movementInput != 0)
        {
            SetCharacterState(BayoStates.pushWalk);
        }

        //Return to normal sprite
        else { SetCharacterState(BayoStates.idle); }
    }

    Quaternion GetNormalFromGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(grab.grabPoint.position, Vector2.down, raycastDistance, jump.ground);
        return Quaternion.FromToRotation(Vector2.up, hit.normal);
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (currentAnimation == animation.name) return;
        anim.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    private void SetCharacterState(BayoStates state)
    {
        if (state == BayoStates.idle)
        {
            SetAnimation(states.idle, true, 1f);
        }
        else if (state == BayoStates.walking)
        {
            SetAnimation(states.walk, true, 1.5f);
        }
        else if (state == BayoStates.jump)
        {
            SetAnimation(states.jump, false, 1f);
        }
        else if (state == BayoStates.ball)
        {
            SetAnimation(states.ball, false, 1f);
        }
        else if (state == BayoStates.climb)
        {
            SetAnimation(states.climb, false, 1f);
        }
        else if (state == BayoStates.pickUp)
        {
            SetAnimation(states.pickUp, false, 2f);
        }
        else if (state == BayoStates.pushWalk)
        {
            SetAnimation(states.pushWalk, true, 1.5f);
        }
        currentState = state;
    }

    private void ResetGrab()
    {
        playGrab = false;
    }
}

[System.Serializable]
public class AnimationStates
{
    public AnimationReferenceAsset idle, walk, pushWalk, jump, pickUp, climb, ball;
}
