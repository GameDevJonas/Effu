using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    private enum ActivePlayer { bayo, cilombo, chiku};
    [SerializeField] private ActivePlayer activePlayer;
    public GameObject[] Characters;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
