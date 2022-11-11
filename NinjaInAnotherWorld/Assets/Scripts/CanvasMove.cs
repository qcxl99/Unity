using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Camera.main.transform.position;
    }
}
