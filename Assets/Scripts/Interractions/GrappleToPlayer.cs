using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleToPlayer : Grapplable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GrabMe()
    {
        base.GrabMe();
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<GrabFollow>().enabled = true;
        FindObjectOfType<PlayerGrab>().grabObj = GetComponent<GrabFollow>();
        FindObjectOfType<PlayerGrab>().Grab();
        transform.SetParent(null);
        Destroy(this);
    }
}
