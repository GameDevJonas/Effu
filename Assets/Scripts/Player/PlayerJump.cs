using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerControls playerControls;
    [HideInInspector] public Rigidbody2D rb;
    private Collider2D col;
    [HideInInspector] public bool inJump, inAir;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpHeight, fallMultiplier, lowJumpMultiplier;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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
        playerControls.Land.Jump.performed += _ => Jump();
        playerControls.Land.Jump.started += _ => inJump = true;
        playerControls.Land.Jump.canceled += _ => inJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            inAir = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !inJump)
        {
            inAir = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (IsGrounded()) { inAir = false;}
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = Vector2.up * jumpHeight;
            inJump = true;
            inAir = true;
        }
    }

    public bool IsGrounded()
    {
        Vector2 topLeft = transform.position;
        topLeft.x -= col.bounds.extents.x;
        topLeft.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeft, bottomRight, ground);
    }
}
