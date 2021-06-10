using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SnareEvent : Snare
{
    [SerializeField] private PlayableDirector director;

    private void Start()
    {
        Invoke("FindNewPlayer", .5f);
    }

    private void FindNewPlayer()
    {
        player = GameObject.Find("Player (1)").transform;
    }

    public override void Trap()
    {
        anim.SetTrigger("Activate");
        source.Play();
        //player = collision.transform.parent.parent;
        player.GetComponent<PlayerInputs>().DisableEnableSnare(false);
        trapBody.bodyType = RigidbodyType2D.Dynamic;
        left.enabled = true;
        right.enabled = true;
        jointLock.enabled = true;
        jointLock.connectedBody = player.GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;
        director.Play();
    }
}
