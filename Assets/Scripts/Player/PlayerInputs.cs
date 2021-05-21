using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerJump jump;
    public PlayerGrab grab;
    public PlayerLedgeClimb climb;
    public PlayerBall ball;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableEnableAll(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        climb.enabled = b;
        ball.enabled = b;
    }

    public void DisableEnableClimb(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        ball.enabled = b;
    }

    public void DisableEnableGrab(bool b)
    {
        climb.enabled = b;
        ball.enabled = b;
    }

    public void DisableEnableBall(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        climb.enabled = b;
    }
}
