using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplable : MonoBehaviour
{
    [HideInInspector] public bool isGrappled;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void GrabMe()
    {

    }
}
