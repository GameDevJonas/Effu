using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : MonoBehaviour
{
    private PlayerControls playerControls;
    public AudioSource callSource;
    public GameObject callVisual;

    public bool inCall;

    public List<CallRespond> respondandts = new List<CallRespond>();

    private void Awake()
    {
        playerControls = new PlayerControls();
        foreach(CallRespond obj in FindObjectsOfType<CallRespond>())
        {
            respondandts.Add(obj);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Land.Call.performed += _ => CallOutNow();
    }

    void Update()
    {

    }

    void CallOutNow()
    {
        if (!inCall) StartCoroutine(CallOut());
    }

    IEnumerator CallOut()
    {
        GetComponent<PlayerAnimations>().CallAnim();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;
        GetComponent<PlayerGrab>().enabled = false;
        inCall = true;
        GameObject visual = Instantiate(callVisual, (GetComponent<PlayerGrab>().grabPoint.position), Quaternion.identity, GameObject.Find("MainCanvas").transform);
        callSource.Play();
        yield return new WaitForSeconds(callSource.clip.length - 1f);
        foreach(CallRespond obj in respondandts)
        {
            obj.Invoke("RespondCall", Random.Range(0, .5f));
        }
        Destroy(visual, 1f);
        inCall = false;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerJump>().enabled = true;
        GetComponent<PlayerGrab>().enabled = true;
        yield return null;
    }
}
