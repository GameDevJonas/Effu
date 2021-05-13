using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFollow : Grabbable
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void GrabMe()
    {
        base.GrabMe();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public override void UnGrab()
    {
        base.UnGrab();
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.position = player.GetComponent<PlayerGrab>().grabPoint.position;
        }
    }
}
