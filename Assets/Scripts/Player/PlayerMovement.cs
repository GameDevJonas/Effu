using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private PlayerControls playerControls;
    private PlayerInputs inputs;
    public float speed;
    public float movementInput;
    private Rigidbody2D rb;
    public float direction;
    public bool isPushing, disableInputs;

    [Header("Audio variables")]
    private PlayerAudio pa;
    [SerializeField] private float stepInterval;
    private float timer;
    private PlayerJump jump;

    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
        pa = GetComponent<PlayerAudio>();
        jump = GetComponent<PlayerJump>();
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
            if(jump.IsGrounded()) Footsteps();
        }
        else if(movementInput == 0)
        {
            timer = 0;
        }
    }

    void Footsteps()
    {
        if(timer >= stepInterval)
        {
            pa.PlayFootstep();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
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
