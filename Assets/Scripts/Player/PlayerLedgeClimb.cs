using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimb : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInputs inputs;
    private Rigidbody2D rb;
    private PlayerMovement movement;
    private PlayerJump jump;
    private Vector3 endPos;

    [SerializeField] private float climbTime = .5f;
    [HideInInspector] public bool canClimb, isClimbing;
    [SerializeField] private BoxCollider2D boxCol;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        inputs = GetComponent<PlayerInputs>();
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
        CheckForInputs();
        if (isClimbing)
        {
            Climbing();
        }
    }

    void CheckForInputs()
    {
        canClimb = movement.movementInput != 0 && jump.inJump;
    }

    public void StartClimb(Vector3 endP)
    {
        endPos = endP;
        boxCol.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        isClimbing = true;
    }

    void Climbing()
    {
        float step = climbTime * Time.deltaTime;
        if (transform.position != endPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            inputs.DisableEnableClimb(false);
        }
        else
        {
            isClimbing = false;
            boxCol.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            inputs.DisableEnableClimb(true);
        }
    }
}
