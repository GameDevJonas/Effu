using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHorizontally : MonoBehaviour
{
    private Transform mainCam;

    private void Awake()
    {
        mainCam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.position = FollowCamera();
    }

    private Vector2 FollowCamera()
    {
        return new Vector2(mainCam.position.x, transform.position.y);
    }
}
