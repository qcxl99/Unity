using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    bool jump;
    bool changeColor;
    PlayerCharacter cha;
    // Start is called before the first frame update
    void Start()
    {
        cha = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");*/
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            changeColor = true;
        }
    }
    private void FixedUpdate()
    {
        cha.Move(jump,changeColor);
        jump = false;
        changeColor = false;
    }
}
