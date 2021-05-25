using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerAnimations anim;
    public PlayerMovement movement;
    public PlayerJump jump;
    public PlayerGrab grab;
    public PlayerLedgeClimb climb;
    public PlayerBall ball;
    public PlayerTongue tongue;

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
        tongue.enabled = b;
    }

    public void DisableEnableClimb(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        ball.enabled = b;
        tongue.enabled = b;
    }

    public void DisableEnableGrab(bool b)
    {
        climb.enabled = b;
        ball.enabled = b;
        tongue.enabled = b;
    }

    public void DisableEnableBall(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        climb.enabled = b;
        tongue.enabled = b;
    }

    public void DisableEnableTongue(bool b)
    {
        movement.enabled = b;
        jump.enabled = b;
        grab.enabled = b;
        climb.enabled = b;
        ball.enabled = b;
    }

    public void MoveToPoint(Transform point)
    {
        Debug.Log("Test");
        StartCoroutine(GoToPoint(point));
    }

    IEnumerator GoToPoint(Transform point)
    {
        float distance = Vector2.Distance(transform.position, point.position);
        movement.direction = transform.position.x - point.position.x;
        while (distance > .5f)
        {
            float step = movement.speed / 100 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point.position, step);
            distance = Vector2.Distance(transform.position, point.position);
            movement.direction = point.position.x - transform.position.x;
            if (movement.direction > 1 || (movement.direction >= 0 && movement.direction <= 1)) movement.direction = 1;
            else if (movement.direction < -1 || (movement.direction < 0 && movement.direction >= -1)) movement.direction = -1;
            anim.pivot.localScale = new Vector3(movement.direction, anim.pivot.localScale.y, anim.pivot.localScale.z);
            yield return new WaitForEndOfFrame();
        }
        movement.direction = transform.position.x - point.position.x;
        if (movement.direction > 1 || (movement.direction >= 0 && movement.direction <= 1)) movement.direction = 1;
        else if (movement.direction < -1 || (movement.direction < 0 && movement.direction >= -1)) movement.direction = -1;
        anim.pivot.localScale = new Vector3(movement.direction, anim.pivot.localScale.y, anim.pivot.localScale.z);
        StopCoroutine(GoToPoint(point));
    }
}