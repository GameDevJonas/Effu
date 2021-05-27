using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappableFall : Grapplable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GrabMe(float distance, float tongueSpeed, LineRenderer line, Transform lineEnd)
    {
        base.GrabMe(distance, tongueSpeed, line, lineEnd);
        transform.SetParent(null);
        EndGrapple();
    }

    public override void EndGrapple()
    {
        base.EndGrapple();
    }
}
