using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform t;
    Vector3 o; //define a line to tie up
    // Start is called before the first frame update
    void Start()
    {
        o = transform.position - t.position; //define the length and angle etc of the line
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = t.position + o; // let the camera will be towed by the line and ball
    }
}
