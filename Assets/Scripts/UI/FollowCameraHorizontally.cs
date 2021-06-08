using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHorizontally : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = FollowCamera();
    }

    private Vector2 FollowCamera()
    {
        return new Vector2(player.position.x, transform.position.y);
    }
}
