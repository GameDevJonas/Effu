using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRope : Grabbable
{
    private DistanceJoint2D joint;

    public override void Awake()
    {
        base.Awake();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }

    public override void GrabMe()
    {
        base.GrabMe();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.transform.position = transform.position;
        //transform.parent = player.transform;
        joint.enabled = true;
        joint.connectedBody = player.GetComponent<Rigidbody2D>();
    }

    public override void UnGrab()
    {
        base.UnGrab();
        joint.enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.y, 0);
        //transform.parent = null;
        joint.connectedBody = null;
    }
}
