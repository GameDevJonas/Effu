using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private float speed, brakeValue;
    [HideInInspector] public float movementInput;
    private Rigidbody2D rb;
    [HideInInspector] public float direction;
    [HideInInspector] public bool turnNow;

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
        movementInput = playerControls.Land.Move.ReadValue<float>();
    }

    void ApplyMovement()
    {
        if (movementInput != 0)
        {
            if (direction != movementInput) turnNow = true; else turnNow = false;
            rb.velocity = new Vector2(movementInput * speed * Time.deltaTime, rb.velocity.y);
            direction = movementInput;
        }
        else if (movementInput == 0 && rb.velocity.x != 0 && GetComponent<PlayerJump>().IsGrounded())
        {
            //rb.velocity = new Vector2(-brakeValue * rb.velocity.x, rb.velocity.y);
            //rb.AddForce(-brakeValue * rb.velocity);
        }
    }
}
