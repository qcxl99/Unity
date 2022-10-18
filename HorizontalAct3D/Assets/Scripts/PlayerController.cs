using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    PlayerCharacter character;

    public bool jump { get; private set; }

    void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        character.Move(h,jump);
        
        if (Input.GetButtonDown("Fire1"))
        {
            character.Grab();
        }

    }
}
