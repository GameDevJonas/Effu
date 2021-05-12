using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerJump jump;
    private PlayerMovement movement;
    private bool doJump;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        jump = GetComponent<PlayerJump>();
        movement = GetComponent<PlayerMovement>();
        doJump = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForJumpAnim();
        CheckForMoveAnim();
    }

    void CheckForJumpAnim()
    {
        anim.SetBool("InJump", jump.inJump);
        anim.SetFloat("Velocity", jump.rb.velocity.y);
    }

    void CheckForMoveAnim()
    {
        anim.SetBool("Idle", (movement.movementInput != 0) ? false : true);
        anim.SetInteger("Direction", (int)movement.direction);
        if (movement.turnNow)
        {
            anim.SetTrigger("Turn");
            movement.turnNow = false;
        }
    }
}
