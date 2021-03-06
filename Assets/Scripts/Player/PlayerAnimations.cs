using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Collections;
using Spine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation anim;
    [SerializeField] private AnimationStates states;
    [SerializeField] private string currentAnimation;
    public bool isMama;
    public enum BayoStates { idle, mangoIdle, walking, mangoWalk, pushWalk, jump, pickUp, climb, ball, tongue, death };
    [SerializeField] private BayoStates currentState;

    [SerializeField] private float raycastDistance;
    [SerializeField] private float rotationSpeed;
    //private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask whatIsGroundRotation;

    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerGrab grab;
    public bool playGrab = false;
    //[SerializeField] private Sprite grabSprite;

    private PlayerLedgeClimb climb;
    //[SerializeField] private Sprite climbSprite;

    private PlayerBall ball;
    //[SerializeField] private Sprite ballSprite, normalSprite;

    private PlayerTongue tongue;

    public Transform pivot;

    public bool mamaDeath;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        grab = GetComponent<PlayerGrab>();
        climb = GetComponent<PlayerLedgeClimb>();
        ball = GetComponent<PlayerBall>();
        tongue = GetComponent<PlayerTongue>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = BayoStates.idle;
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate pivot to direction
        if (movement.direction != 0 && !climb.isClimbing && !ball.isBall)
        {
            float stickRaw = movement.direction;
            float newDir = 1;
            if (stickRaw > 0 && stickRaw <= 1) newDir = 1;
            else if (stickRaw < 0 && stickRaw >= -1) newDir = -1;
            //else newDir = 0;
            pivot.localScale = new Vector3(newDir, pivot.localScale.y, pivot.localScale.z);
        }

        //Change rotation of pivot of normal from ground
        if (jump.IsGrounded() && !ball.isBall && !climb.isClimbing)
        {
            Debug.DrawRay(grab.grabPoint.position, Vector2.down * raycastDistance, Color.red);
            float step = rotationSpeed * Time.deltaTime;
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, GetNormalFromGround(), step);
        }
        else if (!ball.isBall) pivot.rotation = Quaternion.Euler(Vector3.zero);

        //Set walking animation
        if (movement.movementInput != 0 && !ball.isBall && !climb.isClimbing && !jump.inJump && !movement.isPushing && !playGrab) { SetCharacterState(BayoStates.walking); playGrab = false; }

        else if (tongue.tongueAnim)
        {
            SetCharacterState(BayoStates.tongue);
        }

        else if (playGrab && grab.enabled)
        {
            SetCharacterState(BayoStates.pickUp);
            Invoke("ResetGrab", .3f);
        }

        //Set ball animation
        else if (ball.isBall)
        {
            SetCharacterState(BayoStates.ball);
            //pivot.transform.Rotate(Vector3.forward, -GetComponent<Rigidbody2D>().velocity.x);
        }

        //Set climb animation
        else if (climb.isClimbing)
        {
            SetCharacterState(BayoStates.climb);
        }

        else if (jump.inJump && !jump.disableInputs)
        {
            SetCharacterState(BayoStates.jump);
        }

        else if (movement.isPushing && movement.movementInput != 0)
        {
            SetCharacterState(BayoStates.pushWalk);
        }

        //Return to normal sprite
        else if (!mamaDeath) { SetCharacterState(BayoStates.idle); }
        else
        {
            SetCharacterState(BayoStates.death);
        }
    }

    //Flips pivot x rotation
    public void FlipMe()
    {
        movement.direction = -movement.direction;
        //pivot.localScale = new Vector3(-movement.direction, pivot.localScale.y, pivot.localScale.z);
    }

    Quaternion GetNormalFromGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(grab.grabPoint.position, Vector2.down, raycastDistance, whatIsGroundRotation);
        if (hit)
        {
            Quaternion newRot = Quaternion.FromToRotation(Vector2.up, hit.normal);
            //if (newRot.eulerAngles.z > 40) newRot = Quaternion.Euler(0, 0, 40);
            //else if (newRot.eulerAngles.z < -40) newRot = Quaternion.Euler(0, 0, -40);
            return newRot;
        }
        else return Quaternion.Euler(Vector3.zero);
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (currentAnimation == animation.name) return;
        anim.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void PlayMamaThrow()
    {
        mamaDeath = true;
    }

    private void SetCharacterState(BayoStates state)
    {
        if (state == BayoStates.idle)
        {
            if (!grab.isGrabbing)
            {
                if (isMama) SetAnimation(states.mamaStates.idle, true, 1f);
                else if (!isMama) SetAnimation(states.bayoStates.idle, true, 1f);
            }
            else
            {
                if (isMama) SetAnimation(states.mamaStates.mangoIdle, true, 1f);
                else if (!isMama) SetAnimation(states.bayoStates.mangoIdle, true, 1f);
            }
        }
        /*else if (state == BayoStates.mangoIdle)
        {
            if (isMama) SetAnimation(states.mamaStates.mangoIdle, true, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.mangoIdle, true, 1f);
        }*/
        else if (state == BayoStates.walking)
        {
            if (!grab.isGrabbing)
            {
                if (isMama) SetAnimation(states.mamaStates.walk, true, 1f);
                else if (!isMama) SetAnimation(states.bayoStates.walk, true, 1f);
            }
            else
            {
                if (isMama) SetAnimation(states.mamaStates.mangoWalk, true, 1f);
                else if (!isMama) SetAnimation(states.bayoStates.mangoWalk, true, 1f);
            }
        }
        /*else if(state == BayoStates.mangoWalk)
        {
            if (isMama) SetAnimation(states.mamaStates.mangoWalk, true, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.mangoWalk, true, 1f);
        }*/
        else if (state == BayoStates.jump)
        {
            if (isMama) SetAnimation(states.mamaStates.jump, false, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.jump, false, 1f);
        }
        else if (state == BayoStates.ball)
        {
            if (isMama) SetAnimation(states.mamaStates.ball, false, 2f);
            else if (!isMama) SetAnimation(states.bayoStates.ball, false, 2f);
        }
        else if (state == BayoStates.climb)
        {
            if (isMama) SetAnimation(states.mamaStates.climb, false, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.climb, false, 1f);
        }
        else if (state == BayoStates.pickUp)
        {
            if (isMama) SetAnimation(states.mamaStates.pickUp, false, 2f);
            else if (!isMama) SetAnimation(states.bayoStates.pickUp, false, 2f);
        }
        else if (state == BayoStates.pushWalk)
        {
            if (isMama) SetAnimation(states.mamaStates.pushWalk, true, 1.5f);
            else if (!isMama) SetAnimation(states.bayoStates.pushWalk, true, 1.5f);
        }
        else if (state == BayoStates.death)
        {
            if (isMama) SetAnimation(states.mamaStates.death, false, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.death, false, 1f);
        }
        else if(state == BayoStates.tongue)
        {
            if (isMama) SetAnimation(states.mamaStates.tongue, false, 1f);
            else if (!isMama) SetAnimation(states.bayoStates.tongue, false, 1f);
        }
        currentState = state;
    }

    private void ResetGrab()
    {
        playGrab = false;
    }

    [ContextMenu("Switch")]
    public void SwitchToBayo()
    {
        isMama = false;
        anim.skeletonDataAsset = states.bayo;
        anim.ClearState();
    }
}

[System.Serializable]
public class AnimationStates
{
    public PlayerStates mamaStates, bayoStates;
    public SkeletonDataAsset mama, bayo;
}

[System.Serializable]
public class PlayerStates
{
    public AnimationReferenceAsset idle, mangoIdle, walk, mangoWalk, pushWalk, jump, pickUp, climb, tongue, ball, death;
}
