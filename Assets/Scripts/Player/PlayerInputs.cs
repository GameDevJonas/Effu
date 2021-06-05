using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerControls playerControls;
    private MenuManager menu;

    public PlayerAnimations anim;
    public PlayerMovement movement;
    public PlayerJump jump;
    public PlayerGrab grab;
    public PlayerLedgeClimb climb;
    public PlayerBall ball;
    public PlayerTongue tongue;

    private void Awake()
    {
        playerControls = new PlayerControls();
        menu = FindObjectOfType<MenuManager>();
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
        playerControls.Land.Menu.performed += _ => PauseGame();
    }

    private void PauseGame()
    {
        menu.PauseGame();
    }

    public void DisableEnableAll(bool b)
    {
        if (ball.isBall) ball.BallQuit();
        movement.disableInputs = !b;
        jump.disableInputs = !b;
        grab.enabled = b;
        climb.enabled = b;
        ball.disableInputs = !b;
        tongue.enabled = b;
    }

    public void DisableEnableClimb(bool b)
    {
        movement.disableInputs = !b;
        jump.disableInputs = !b;
        grab.enabled = b;
        ball.disableInputs = !b;
        tongue.enabled = b;
    }

    public void DisableEnableGrab(bool b)
    {
        climb.enabled = b;
        ball.disableInputs = !b;
        tongue.enabled = b;
    }

    public void DisableEnableBall(bool b)
    {
        movement.disableInputs = !b;
        jump.disableInputs = !b;
        grab.enabled = b;
        climb.enabled = b;
        tongue.enabled = b;
    }

    public void DisableEnableTongue(bool b)
    {
        movement.disableInputs = !b;
        jump.disableInputs = !b;
        grab.enabled = b;
        climb.enabled = b;
        ball.disableInputs = !b;
    }

    public void DisableEnableSnare(bool b)
    {
        grab.enabled = b;
        climb.enabled = b;
        tongue.enabled = b;
    }

    public void DisableEnableCutscene(bool b)
    {
        if (ball.isBall) ball.BallQuit();
        movement.disableInputs = !b;
        jump.disableInputs = !b;
        grab.enabled = b;
        climb.enabled = b;
        ball.disableInputs = !b;
        if (!b) StopCoroutine(GoToPoint(null));
    }

    public void MoveToPoint(Transform point)
    {
        StartCoroutine(GoToPoint(point));
    }

    IEnumerator GoToPoint(Transform point)
    {
        DisableEnableCutscene(false);
        float distance = Vector2.Distance(transform.position, point.position);
        movement.direction = transform.position.x - point.position.x;
        while (distance > .5f)
        {
            distance = Vector2.Distance(transform.position, point.position);
            //Debug.Log(distance);

            movement.movementInput = Mathf.RoundToInt(Mathf.Clamp((point.position.x - transform.position.x), -1, 1));
            yield return new WaitForEndOfFrame();
        }
        //movement.movementInput = -movement.direction;
        yield return new WaitForSeconds(.1f);
        anim.FlipMe();
        DisableEnableCutscene(true);
        StopCoroutine(GoToPoint(point));
    }

}
