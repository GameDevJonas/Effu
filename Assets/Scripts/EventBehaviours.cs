using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBehaviours : MonoBehaviour
{
    public void ParentDynamicRigidbody(GameObject target)
    {
        target.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
