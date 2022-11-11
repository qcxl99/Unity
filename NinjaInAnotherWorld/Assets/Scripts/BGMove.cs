using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public Transform Player,BG, FG;
    Vector3 distance;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        BG.position = transform.position / 3 + new Vector3(220,0,0);
        FG.position = transform.position / 3 + new Vector3(0, -40, 0);
    }
}
