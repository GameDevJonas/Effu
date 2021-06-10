using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimb : MonoBehaviour
{
    private PlayerInputs inputs;
    private Rigidbody2D rb;
    private PlayerMovement movement;
    private PlayerJump jump;
    private Vector3 endPos;

    [SerializeField] private float climbTime = .5f;
    public bool canClimb, isClimbing, inRange;
    [SerializeField] private BoxCollider2D boxCol;

    private PlayerAudio pa;
    [Range(0, 100)]
    [SerializeField] private float probability;


    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        pa = GetComponent<PlayerAudio>();
        
    }
    private void Start()
    {
        //StartClimb(transform.position + Vector3.left * 2);
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
        inputs.DisableEnableClimb(false);
        movement.movementInput = 0;
        rb.bodyType = RigidbodyType2D.Static;
        endPos = endP;
        boxCol.enabled = false;
        isClimbing = true;
        pa.PlayClimb(probability);
    }

    void Climbing()
    {
        float step = climbTime * Time.deltaTime;
        //Debug.Log(step);
        if (Vector2.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, step);
        }
        else
        {
            isClimbing = false;
            boxCol.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            inputs.DisableEnableClimb(true);
            //Debug.Log("Test");
        }
    }
}
