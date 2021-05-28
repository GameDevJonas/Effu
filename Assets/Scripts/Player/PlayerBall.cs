using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInputs inputs;
    [HideInInspector] public bool isBall;
    private Rigidbody2D rb;
    private CircleCollider2D colC;
    private BoxCollider2D colB;

    private void Awake()
    {
        playerControls = new PlayerControls();
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
        colC = GetComponent<CircleCollider2D>();
        colB = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Land.Ball.performed += _ => BallStart();
        playerControls.Land.Ball.canceled += _ => BallQuit();
    }

    void BallStart()
    {
        isBall = true;
        inputs.DisableEnableBall(false);
        rb.constraints = RigidbodyConstraints2D.None;
        colC.enabled = true;
        colB.enabled = false;
    }

    void BallQuit()
    {
        isBall = false;
        inputs.DisableEnableBall(true);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        colC.enabled = false;
        colB.enabled = true;
    }
}
