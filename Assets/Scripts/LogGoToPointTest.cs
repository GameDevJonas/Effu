using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogGoToPointTest : MonoBehaviour
{
    [SerializeField] private Transform goToPoint;
    public bool turnOn;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turnOn && Vector2.Distance(transform.position, goToPoint.position) > .2f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, goToPoint.position, step);
        }
    }
}
