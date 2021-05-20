using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement movement;
    public Transform sprite;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(movement.direction != 0)
        {
            sprite.localScale = new Vector3(movement.direction, sprite.localScale.y, sprite.localScale.z);
        }
    }
}
