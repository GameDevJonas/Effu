using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongue : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInputs inputs;
    private bool inGrapple, performed, inRange;
    [SerializeField] private GameObject marker;
    private List<Grapplable> grapplables = new List<Grapplable>();
    private Queue<Grapplable> grapplablesQueue = new Queue<Grapplable>();

    [SerializeField] private Grapplable target;
    [SerializeField] private float maxDistance, tongueSpeed;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform lineEnd;
    [SerializeField] private bool showRange;

    private void Awake()
    {
        playerControls = new PlayerControls();
        inputs = GetComponent<PlayerInputs>();
        performed = true;
        inRange = true;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Land.Grapple.performed += _ => inGrapple = !inGrapple;
    }

    private void Update()
    {
        if (inGrapple)
        {
            GrappleStart();
        }
        else if (!inGrapple && !performed)
        {
            GrappleStop();
        }
        line.SetPosition(0, line.transform.position);
        line.SetPosition(1, lineEnd.transform.position);
    }

    private void GrappleStart()
    {
        line.enabled = true;
        line.SetPosition(0, line.transform.position);
        line.SetPosition(1, lineEnd.transform.position);
        performed = false;
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        inputs.DisableEnableTongue(false);
        if (FindObjectsOfType<Grapplable>().Length > 0 && grapplables.Count < 1)
        {
            GetGrapplables();
        }
        if (grapplables.Count > 0 && !target && inRange)
        {
            FindClosest();
        }
        if (target)
        {
            marker.SetActive(true);
            marker.transform.position = target.transform.position;
        }
    }

    private void GrappleStop()
    {
        StartCoroutine(TonguePerform());
    }

    IEnumerator TonguePerform()
    {
        marker.SetActive(false);
        if (!target)
        {
            line.enabled = false;
            inputs.DisableEnableTongue(true);
            performed = true;
            target = null;
            StopCoroutine(TonguePerform());
            yield return null;
        }
        float distance = Vector2.Distance(line.transform.position, target.transform.position);
        while (lineEnd.position != target.transform.position)
        {
            float step = tongueSpeed * Time.deltaTime;
            lineEnd.position = Vector2.MoveTowards(lineEnd.position, target.transform.position, step);
            yield return new WaitForEndOfFrame();
        }
        while(distance > .5f)
        {
            float step = tongueSpeed * Time.deltaTime;
            target.transform.position = Vector2.MoveTowards(target.transform.position, line.transform.position, step);
            yield return new WaitForEndOfFrame();
        }
        line.enabled = false;
        lineEnd.position = line.transform.position;
        grapplables.Remove(target);
        target.GrabMe();
        inputs.DisableEnableTongue(true);
        performed = true;
        target = null;
    }

    private void GetGrapplables()
    {
        foreach (Grapplable obj in FindObjectsOfType<Grapplable>())
        {
            grapplables.Add(obj);
            grapplablesQueue.Enqueue(obj);
        }
    }

    private void FindClosest()
    {
        Grapplable closest;
        float distance = maxDistance + 10;
        foreach (Grapplable obj in grapplables)
        {
            float newDistance = Vector2.Distance(transform.position, obj.transform.position);
            if (newDistance < distance)
            {
                closest = obj;
                distance = newDistance;
                target = closest;
            }
        }
        if (distance > maxDistance)
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }
}
