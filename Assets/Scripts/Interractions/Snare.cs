using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snare : MonoBehaviour
{
    [HideInInspector] public SpringJoint2D joint;
    [HideInInspector] public LineRenderer line;
    [HideInInspector] public Transform player;
    [HideInInspector] public ParticleSystem destroyParticles;
    [HideInInspector] public bool isDisarmed = false;
    [HideInInspector] public Animator anim;
    [HideInInspector] public AudioSource source;

    public BoxCollider2D left, right;
    public Joint2D jointLock;
    public Rigidbody2D trapBody;
    public Transform basePoint;

    public void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
        line = GetComponentInChildren<LineRenderer>();
        destroyParticles = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        source = GetComponentInChildren<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void Update()
    {
        if (jointLock.enabled)
        {
            line.SetPosition(0, trapBody.transform.position);
            line.SetPosition(1, basePoint.position);
        }
        else
        {
            line.SetPosition(0, trapBody.transform.position);
            line.SetPosition(1, basePoint.position);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Grabbable") || collision.CompareTag("Disarm")) && !isDisarmed)
        {
            //Play disarm anim or sumthn
            if (collision.GetComponent<Grabbable>().isGrabbed) collision.GetComponent<Grabbable>().UnGrab();
            collision.GetComponent<Grabbable>().enabled = false;
            Disarm(collision);
        }
        else if ((collision.transform == player || collision.transform.parent.parent == player) && !isDisarmed)
        {
            Trap();
            //this.enabled = false;
        }
    }

    public void Disarm(Collider2D collision)
    {
        anim.SetTrigger("Activate");
        source.Play();
        trapBody.bodyType = RigidbodyType2D.Dynamic;
        left.enabled = true;
        right.enabled = true;
        jointLock.enabled = true;
        jointLock.connectedBody = collision.GetComponent<Rigidbody2D>();
        destroyParticles.Play();
        isDisarmed = true;
    }

    public virtual void Trap()
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
        StartCoroutine(DeathRoutine());
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator DeathRoutine()
    {
        float timer = 0;
        float deathTime = 5;
        while (timer <= deathTime)
        {
            timer += Time.deltaTime;
            GameObject.Find("DeathFader").GetComponent<Image>().color += new Color(0, 0, 0, 0.001f);
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<MenuManager>().ReloadScene();
    }
}
