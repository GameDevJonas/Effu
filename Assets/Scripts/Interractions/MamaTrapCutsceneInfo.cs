using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaTrapCutsceneInfo : MonoBehaviour
{
    [SerializeField] private GameObject mamaPlayer, bayoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mamaPlayer.SetActive(true);
        bayoPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
