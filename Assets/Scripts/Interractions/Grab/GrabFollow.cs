using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFollow : Grabbable
{
    [Range(0, 1)]
    [SerializeField] private float dropDistance;

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
        int dir = (int)player.GetComponent<PlayerMovement>().direction;
        transform.position += Vector3.left * -dir * dropDistance;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //GetComponent<Rigidbody2D>().AddForce(Vector3.left * -dir * dropForce, ForceMode2D.Impulse);
        //player.GetComponent<PlayerGrab>().grabObj = null;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.position = player.GetComponentInChildren<PlayerGrab>().grabPoint.position;
        }
    }
}
