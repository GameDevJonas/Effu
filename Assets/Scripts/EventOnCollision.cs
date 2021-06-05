using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    [SerializeField] private bool disableAfterInvoked = true;
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
        if ((collision.gameObject.layer.Equals(7) && collision.transform.parent.parent.CompareTag("Player")) && !FindObjectOfType<PlayerBall>().isBall)
        {
            eventToInvoke.Invoke();
            if(disableAfterInvoked) this.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Player"))
        {
            eventToInvoke.Invoke();
            if (disableAfterInvoked) this.gameObject.SetActive(false);
        }
    }
}
