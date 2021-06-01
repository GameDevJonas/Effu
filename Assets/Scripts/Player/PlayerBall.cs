using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private PlayerInputs inputs;
    public bool isBall, disableInputs;
    private Rigidbody2D rb;
    private CircleCollider2D colC;
    private BoxCollider2D colB;

    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody2D>();
        colC = GetComponent<CircleCollider2D>();
        colB = GetComponentInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        inputs.playerControls.Land.Ball.performed += _ => BallStart();
        inputs.playerControls.Land.Ball.canceled += _ => BallQuit();
    }

    void BallStart()
    {
        if (disableInputs) return;
        isBall = true;
        inputs.DisableEnableBall(false);
        rb.constraints = RigidbodyConstraints2D.None;
        colC.enabled = true;
        colB.enabled = false;
    }

    void BallQuit()
    {
        if (disableInputs) return;
        isBall = false;
        inputs.DisableEnableBall(true);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        colC.enabled = false;
        colB.enabled = true;
    }
}
