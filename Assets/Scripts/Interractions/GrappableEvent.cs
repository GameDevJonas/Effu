using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrappableEvent : Grapplable
{
    public UnityEvent events;
    private Transform lineEndT;
    private bool inEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inEvent) lineEndT.position = transform.position;
    }

    public override void GrabMe(float distance, float tongueSpeed, LineRenderer line, Transform lineEnd)
    {
        lineEndT = lineEnd;
        base.GrabMe(distance, tongueSpeed, line, lineEnd);
        inEvent = true;
        events.Invoke();
        //EndGrapple();
    }

    public override void EndGrapple()
    {
        inEvent = false;
        base.EndGrapple();
    }

    public void DynamicParent()
    {
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public void StaticParent()
    {
        EndGrapple();
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
