using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.parent && collision.transform.parent.parent.CompareTag("Player"))
        {
            eventToInvoke.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
