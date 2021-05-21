using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    private SpriteRenderer spriteRenderer;

    private PlayerMovement movement;

    private PlayerJump jump;

    private PlayerGrab grab;
    [SerializeField] private Sprite grabSprite;

    private PlayerLedgeClimb climb;
    [SerializeField] private Sprite climbSprite;

    private PlayerBall ball;
    [SerializeField] private Sprite ballSprite, normalSprite;

    public Transform pivot;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        grab = GetComponent<PlayerGrab>();
        climb = GetComponent<PlayerLedgeClimb>();
        ball = GetComponent<PlayerBall>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rotate pivot to direction
        if (movement.direction != 0 && !climb.isClimbing)
        {
            float stickRaw = movement.direction;
            float newDir = 1;
            if (stickRaw > 0 && stickRaw <= 1) newDir = 1;
            else if (stickRaw < 0 && stickRaw >= -1) newDir = -1;
            else newDir = 0;
            pivot.localScale = new Vector3(newDir, pivot.localScale.y, pivot.localScale.z);
        }

        //Change rotation of pivot of normal from ground
        if (jump.IsGrounded() && !ball.isBall)
        {
            Debug.DrawRay(grab.grabPoint.position, Vector2.down * raycastDistance, Color.red);
            pivot.transform.rotation = GetNormalFromGround();
            //Quaternion.Euler(pivot.transform.rotation.x, pivot.transform.rotation.y, GetNormalFromGround().eulerAngles.z)
        }

        //Change sprite to grab-sprite when grabbing
        if (grab.isGrabbing)
        {
            spriteRenderer.sprite = grabSprite;
        }

        //Change sprite of player when ball or not
        else if (ball.isBall)
        {
            spriteRenderer.sprite = ballSprite;
        }

        //Change sprite to climb sprite when climbing
        else if (climb.isClimbing)
        {
            spriteRenderer.sprite = climbSprite;
        }

        //Return to normal sprite
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    Quaternion GetNormalFromGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(grab.grabPoint.position, Vector2.down, raycastDistance, jump.ground);
        return Quaternion.FromToRotation(Vector2.up, hit.normal);
    }
}
