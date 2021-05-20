using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    public Transform grabPoint, endPoint;
    [SerializeField] private float grabPointRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    public bool playerInRange;
    private PlayerLedgeClimb player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerLedgeClimb>();
    }

    void Update()
    {
        playerInRange = CheckForPlayer();
        if (playerInRange && player.canClimb && !player.isClimbing)
        {
            player.StartClimb(endPoint.position);
        }
    }

    bool CheckForPlayer()
    {
        Collider2D col = Physics2D.OverlapCircle(grabPoint.position, grabPointRadius, whatIsPlayer);
        return col;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(grabPoint.position, grabPointRadius);
    }
}
