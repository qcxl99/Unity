using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    Material bg;
    public float speed;
    
    void Start()
    {
        //sharedMaterial: Shares the settings to other bg imgs.
        bg = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        // Offset of bg
        bg.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
