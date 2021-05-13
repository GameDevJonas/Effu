using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondArrowPoint : MonoBehaviour
{
    [SerializeField] private Camera uiCam;

    public Transform target;
    public Vector3 targetPosition;
    private RectTransform arrowRectTransform;

    private void Awake()
    {
        uiCam = Camera.main;
        arrowRectTransform = transform.Find("ArrowPivot").GetComponent<RectTransform>();
        arrowRectTransform.gameObject.SetActive(false);
    }
    private void Start()
    {
        targetPosition = target.position;
    }

    private void Update()
    {
        targetPosition = target.position;
        Vector3 toPos = targetPosition;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.z = 0;
        Vector3 dir = (toPos - fromPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        arrowRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 200f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        if (isOffScreen)
        {
            arrowRectTransform.gameObject.SetActive(true);
            Vector3 capTargetScreenPos = targetPositionScreenPoint;
            if (capTargetScreenPos.x <= borderSize) capTargetScreenPos.x = borderSize;
            if (capTargetScreenPos.x >= Screen.width - borderSize) capTargetScreenPos.x = Screen.width - borderSize;
            if (capTargetScreenPos.y <= borderSize) capTargetScreenPos.y = borderSize;
            if (capTargetScreenPos.y >= Screen.height - borderSize) capTargetScreenPos.y = Screen.height - borderSize;

            Vector3 pointerWorldPos = uiCam.ScreenToWorldPoint(capTargetScreenPos);
            RectTransform myT = GetComponent<RectTransform>();
            myT.position = pointerWorldPos;
            myT.localPosition = new Vector3(myT.localPosition.x, myT.localPosition.y, 0);

        }
        else
        {
            arrowRectTransform.gameObject.SetActive(false);
            RectTransform myT = GetComponent<RectTransform>();
            myT.position = target.GetComponent<CallRespond>().callPoint.position;
            myT.localPosition = new Vector3(myT.localPosition.x, myT.localPosition.y, 0);
        }
    }
}
