using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    public float speed, brakeValue;
    [HideInInspector] public float movementInput;
    private Rigidbody2D rb;
    public float direction;
    public bool isPushing;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {

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
        float stickRaw = playerControls.Land.Move.ReadValue<float>();
        movementInput = stickRaw;
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
