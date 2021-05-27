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

    public override void GrabMe(float distance, float tongueSpeed, LineRenderer line, Transform lineEnd)
    {
        base.GrabMe(distance, tongueSpeed, line, lineEnd);
        StartCoroutine(MoveToPlayer(distance, tongueSpeed, line, lineEnd));
    }

    IEnumerator MoveToPlayer(float distance, float tongueSpeed, LineRenderer line, Transform lineEnd)
    {
        while (distance > .5f)
        {
            distance = Vector2.Distance(line.transform.position, transform.position);
            float step = tongueSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, line.transform.position, step);
            lineEnd.position = transform.position;
            //Debug.Log(distance);
            yield return new WaitForEndOfFrame();
        }
        EndGrapple();
        yield return null;
    }

    public override void EndGrapple()
    {
        Debug.Log("Test");
        base.EndGrapple();
        GetComponent<GrabFollow>().enabled = true;
        FindObjectOfType<PlayerGrab>().grabObj = GetComponent<GrabFollow>();
        FindObjectOfType<PlayerGrab>().Grab();
        transform.SetParent(null);
        Destroy(this);
    }
}
