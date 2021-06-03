using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private PlayerControls playerControls;
    private PlayerInputs inputs;
    public float speed, brakeValue;
    public float movementInput;
    private Rigidbody2D rb;
    public float direction;
    public bool isPushing, disableInputs;

    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    void GetInputs()
    {
        //Read movement value
        if (!disableInputs)
        {
            float stickRaw = inputs.playerControls.Land.Move.ReadValue<float>();
            movementInput = stickRaw;
        }
        else
        {
            movementInput = 0;
        }
    }

    void ApplyMovement()
    {
        if (movementInput != 0)
        {
            rb.velocity = new Vector2(movementInput * speed * Time.deltaTime, rb.velocity.y);
            direction = movementInput;
        }
        //else if (movementInput == 0 && rb.velocity.x != 0 && GetComponent<PlayerJump>().IsGrounded())
        //{
        //    rb.velocity = new Vector2(-brakeValue * rb.velocity.x, rb.velocity.y);
        //    rb.AddForce(-brakeValue * rb.velocity);
        //}
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable")) isPushing = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable")) isPushing = false;
    }
}
