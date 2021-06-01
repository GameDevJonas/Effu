using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GrappableEvent : Grapplable
{
    public UnityEvent events;
    private Transform lineEndT;
    private bool inEvent;

    [SerializeField] private LogCutsceneInfo cutsceneInfo;

    private void Awake()
    {
        cutsceneInfo.cutsceneCam = GameObject.FindGameObjectWithTag("CutsceneCam").GetComponent<CinemachineVirtualCamera>();
        cutsceneInfo.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
    }
    private void Start()
    {
        if (cutsceneInfo.cutsceneCam.gameObject.activeInHierarchy)
        {
            cutsceneInfo.cutsceneCam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inEvent) lineEndT.position = transform.position;
    }

    public override void GrabMe(float distance, float tongueSpeed, LineRenderer line, Transform lineEnd)
    {
        lineEndT = lineEnd;
        base.GrabMe(distance, tongueSpeed, line, lineEnd);
        inEvent = true;
        events.Invoke();
        //EndGrapple();
    }

    public override void EndGrapple()
    {
        inEvent = false;
        base.EndGrapple();
    }

    public void DynamicParent()
    {
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public void StaticParent()
    {
        EndGrapple();
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void NormalMode()
    {
        cutsceneInfo.sideWalls.SetActive(true);
        cutsceneInfo.capCol.enabled = false;
        cutsceneInfo.boxCol.enabled = true;
        cutsceneInfo.cutsceneCam.gameObject.SetActive(false);
        cutsceneInfo.cutsceneCam.Follow = cutsceneInfo.player.transform;
        cutsceneInfo.player.DisableEnableCutscene(true);
    }

    public void PlayerMove()
    {
        cutsceneInfo.player.DisableEnableCutscene(false);
        cutsceneInfo.player.MoveToPoint(cutsceneInfo.playerGoTo);
        cutsceneInfo.cutsceneCam.gameObject.SetActive(true);
        cutsceneInfo.cutsceneCam.Follow = cutsceneInfo.cutsceneFocus;

    }
}

[System.Serializable]
public class LogCutsceneInfo
{
    public GameObject sideWalls;
    public CapsuleCollider2D capCol;
    public BoxCollider2D boxCol;
    [HideInInspector] public CinemachineVirtualCamera cutsceneCam;
    [HideInInspector] public PlayerInputs player;
    public Transform playerGoTo;
    public Transform cutsceneFocus;

}
