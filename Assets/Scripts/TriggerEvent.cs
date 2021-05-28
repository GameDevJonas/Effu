using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnterEvent;
    [SerializeField] private bool executeOnce;
    [SerializeField] private string tagToCheck;

    private bool executed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (executeOnce && executed) return;
        if (collision.CompareTag(tagToCheck))
        {
            onEnterEvent.Invoke();
            executed = true;
        }
    }
}
