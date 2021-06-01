using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private PlayerInputs inputs;
    private PlayerAnimations anim;
    public Transform grabPoint;
    public bool isGrabbing, doAnim;
    public Grabbable grabObj;

    private void Awake()
    {
        doAnim = false;
        inputs = GetComponent<PlayerInputs>();
        anim = GetComponent<PlayerAnimations>();
    }

    void Start()
    {
        inputs.playerControls.Land.Grab.performed += _ => Grab();
        inputs.playerControls.Land.Grab.performed += _ => anim.playGrab = true;
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
