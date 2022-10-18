using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGif : MonoBehaviour
{
    Renderer render;
    PlayerColor color;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponentInChildren<Renderer>();
        render.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ChangeColor();
        }
        void ChangeColor()
        {
            if (color == PlayerColor.Red)
            {
                color = PlayerColor.Green;
                render.material.color = Color.green;
            }
            else
            {
                color = PlayerColor.Red;
                render.material.color = Color.red;
            }
            
        }
    }
}
