using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private UnityEvent onEnterEvent;
    [SerializeField] private UnityEvent onExitEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerTongueLength(float range)
    {
        FindObjectOfType<PlayerTongue>().SetRange(range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.CompareTag("Player")) onEnterEvent.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.parent.parent.CompareTag("Player")) cam.Priority = 15;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.CompareTag("Player"))
        {
            onExitEvent.Invoke();
            cam.Priority = 8;
        }
    }
}
