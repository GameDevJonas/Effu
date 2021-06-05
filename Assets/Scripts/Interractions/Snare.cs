using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snare : MonoBehaviour
{
    private SpringJoint2D joint;
    private LineRenderer line;
    private Transform player;
    private ParticleSystem destroyParticles;
    private bool isDisarmed = false;
    private Animator anim;

    [SerializeField] private BoxCollider2D left, right;
    [SerializeField] private Joint2D jointLock;
    [SerializeField] private Rigidbody2D trapBody;
    [SerializeField] private Transform basePoint;

    private void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
        line = GetComponentInChildren<LineRenderer>();
        destroyParticles = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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
        if((collision.CompareTag("Grabbable") || collision.CompareTag("Disarm")) && !isDisarmed)
        {
            //Play disarm anim or sumthn
            if (collision.GetComponent<Grabbable>().isGrabbed) collision.GetComponent<Grabbable>().UnGrab();
            collision.GetComponent<Grabbable>().enabled = false;
            anim.SetTrigger("Activate");
            trapBody.bodyType = RigidbodyType2D.Dynamic;
            left.enabled = true;
            right.enabled = true;
            jointLock.enabled = true;
            jointLock.connectedBody = collision.GetComponent<Rigidbody2D>();
            destroyParticles.Play();
            isDisarmed = true;
        }
        else if ((collision.transform == player || collision.transform.parent.parent == player) && !isDisarmed)
        {
            anim.SetTrigger("Activate");
            //player = collision.transform.parent.parent;
            player.GetComponent<PlayerInputs>().DisableEnableSnare(false);
            trapBody.bodyType = RigidbodyType2D.Dynamic;
            left.enabled = true;
            right.enabled = true;
            jointLock.enabled = true;
            jointLock.connectedBody = player.GetComponent<Rigidbody2D>();
            StartCoroutine(DeathRoutine());
            GetComponent<Collider2D>().enabled = false;
            //this.enabled = false;
        }
    }

    IEnumerator DeathRoutine()
    {
        float timer = 0;
        float deathTime = 5;
        while(timer <= deathTime)
        {
            timer += Time.deltaTime;
            GameObject.Find("DeathFader").GetComponent<Image>().color += new Color(0, 0, 0, 0.001f);
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<MenuManager>().ReloadScene();
    }
}
