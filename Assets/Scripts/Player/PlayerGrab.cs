using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerAnimations anim;
    private PlayerInputs inputs;
    public Transform grabPoint;
    public bool isGrabbing, doAnim;
    public Grabbable grabObj;

    private void Awake()
    {
        doAnim = false;
        playerControls = new PlayerControls();
        anim = GetComponent<PlayerAnimations>();
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
        playerControls.Land.Grab.performed += _ => Grab();
        playerControls.Land.Grab.performed += _ => anim.playGrab = true;
    }

    public void Grab()
    {
        if (grabObj != null)
        {
            grabObj.Grab();
            inputs.DisableEnableGrab(!grabObj.isGrabbed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable") && !isGrabbing)
        {
            grabObj = collision.GetComponent<Grabbable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable") && !collision.GetComponent<Grabbable>().isGrabbed && !isGrabbing)
        {
            grabObj = null;
        }
    }
}
