using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snare : MonoBehaviour
{
    private SpringJoint2D joint;
    private LineRenderer line;
    private Transform player;
    private ParticleSystem destroyParticles;
    private bool isDisarmed = false;

    private void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
        line = GetComponentInChildren<LineRenderer>();
        destroyParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.enabled)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, player.position);
        }
        else
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDisarmed)
        {
            player = collision.transform;
            joint.enabled = true;
            //this.enabled = false;
        }
        else if((collision.CompareTag("Grabbable") || collision.CompareTag("Disarm")) && !isDisarmed)
        {
            //Play disarm anim or sumthn
            Destroy(collision.gameObject);
            destroyParticles.Play();
            GetComponentInChildren<SpriteRenderer>().color = Color.black;
            isDisarmed = true;
        }
    }
}
