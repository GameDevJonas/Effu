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
        cutsceneInfo.capColT.enabled = false;
        cutsceneInfo.edgeCol.enabled = true;
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

    public void ThrowBreakLog()
    {
        cutsceneInfo.breakLog = GetComponent<Rigidbody2D>();
        cutsceneInfo.breakLog.bodyType = RigidbodyType2D.Dynamic;
        cutsceneInfo.breakLog.AddForce(transform.up * cutsceneInfo.breakLogForce);
        foreach(Rigidbody2D rock in cutsceneInfo.rocks)
        {
            rock.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void FloatLogGoToPoint()
    {
        cutsceneInfo.log.turnOn = !cutsceneInfo.log.turnOn;
    }

    public void FloatNormalMode()
    {
        EndGrapple();
        cutsceneInfo.cutsceneCam.gameObject.SetActive(false);
        cutsceneInfo.cutsceneCam.Follow = cutsceneInfo.player.transform;
        cutsceneInfo.player.DisableEnableCutscene(true);
    }
}

[System.Serializable]
public class LogCutsceneInfo
{
    [Header("Common")]
    [HideInInspector] public CinemachineVirtualCamera cutsceneCam;
    [HideInInspector] public PlayerInputs player;

    [Header("Log")]
    public GameObject sideWalls;
    public CapsuleCollider2D capCol;
    public CapsuleCollider2D capColT;
    public EdgeCollider2D edgeCol;
    public Transform playerGoTo;
    public Transform cutsceneFocus;

    [Header("Rock")]
    public List<Rigidbody2D> rocks = new List<Rigidbody2D>();
    public LogGoToPointTest log;
    public Rigidbody2D breakLog;
    public float breakLogForce;
}
