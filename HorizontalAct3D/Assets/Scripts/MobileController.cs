using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
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
        float h = VirtualContorller.GetAxis(ButtonType.horizontal);
        jump = VirtualContorller.GetButtonDown(ButtonType.jump);
        character.Move(h,jump);
        
        if (VirtualContorller.GetButtonDown(ButtonType.action))
        {
            character.Grab();
        }

    }
}
