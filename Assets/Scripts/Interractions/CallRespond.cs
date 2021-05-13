using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRespond : MonoBehaviour
{
    private Transform player;

    public float targetDistance;
    public float distance;

    public GameObject callVisual;
    public Transform callPoint;
    public AudioSource callSource;
    private bool inCall;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetDistance();
    }

    void GetDistance()
    {
        distance = Vector2.Distance(transform.position, player.position);
    }

    public void RespondCall()
    {
        if(distance <= targetDistance && !inCall)
        {
            StartCoroutine(CallOut());
        }
    }

    IEnumerator CallOut()
    {
        inCall = true;
        GameObject visual = Instantiate(callVisual, (callPoint.position), Quaternion.identity, GameObject.Find("MainCanvas").transform);
        visual.GetComponent<RespondArrowPoint>().target = transform;
        callSource.Play();
        yield return new WaitForSeconds(callSource.clip.length - 1f);
        Destroy(visual, 1f);
        inCall = false;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetDistance);
    }
}
