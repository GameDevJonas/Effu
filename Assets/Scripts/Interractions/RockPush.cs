using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour
{
    [SerializeField] private Transform leftPoint, rightPoint, sprite;
    [SerializeField] private float range;
    [SerializeField] private bool showRange;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private ParticleSystem leftSystem, rightSystem;
    private PlayerMovement movement;
    private Animator anim;
    private bool leftPush, rightPush;

    private void Awake()
    {
        movement = FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftPush = CheckPush(leftPoint, 1);
        rightPush = CheckPush(rightPoint, -1);

        anim.SetBool("LeftPush", leftPush);
        anim.SetBool("RightPush", rightPush);

        if (leftPush && !leftSystem.isPlaying) leftSystem.Play();
        else if(!leftPush) leftSystem.Stop();
        if (rightPush && !rightSystem.isPlaying) rightSystem.Play();
        else if(!rightPush) rightSystem.Stop();
    }

    private bool CheckPush(Transform point, int dirToPush)
    {
        Collider2D col = Physics2D.OverlapCircle(point.position, range, whatIsPlayer);
        return col && movement.movementInput == dirToPush;
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(leftPoint.position, range);
            Gizmos.DrawWireSphere(rightPoint.position, range);
        }
    }
}
