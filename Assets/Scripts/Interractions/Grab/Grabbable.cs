using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isGrabbed;
    [HideInInspector] public GameObject player;

    public virtual void Awake()
    {
        player = GameObject.Find("Player");
    }

    public virtual void Grab()
    {
        if (isGrabbed)
        {
            UnGrab();
            return;
        }
        else if (!isGrabbed)
        {
            GrabMe();
            return;
        }
    }

    public virtual void GrabMe()
    {
        isGrabbed = true;
        player.GetComponentInChildren<PlayerGrab>().isGrabbing = true;
    }

    public virtual void UnGrab()
    {
        player.GetComponentInChildren<PlayerGrab>().UnGrab();
        isGrabbed = false;
    }
}
