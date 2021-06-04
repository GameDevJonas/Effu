using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerInputs inputs;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [HideInInspector] public Rigidbody2D rb;
    private Collider2D col;
    public bool inJump, inAir, disableInputs;

    public LayerMask ground;
    [SerializeField] private float jumpHeight, fallMultiplier, lowJumpMultiplier;

    private PlayerAudio pa;

    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        pa = GetComponent<PlayerAudio>();
    }

    void Start()
    {
        inputs.playerControls.Land.Jump.performed += _ => Jump();
        inputs.playerControls.Land.Jump.started += _ => inJump = true;
        inputs.playerControls.Land.Jump.canceled += _ => inJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disableInputs) return;

        if (rb.velocity.y < 0)
        {
            inAir = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (IsGrounded()) { inAir = false;}

        //if (inAir && inJump)
        //{
        //    if (IsGrounded()) inJump = false;
        //}
    }

    void Jump()
    {
        if (IsGrounded() && !disableInputs)
        {
            pa.PlayJump();
            rb.AddForce(Vector2.up * jumpHeight);
            //rb.velocity = Vector2.up * jumpHeight;
            //inJump = true;
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

        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);

        //return Physics2D.OverlapArea(topLeft, bottomRight, ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
