using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public float explosionTime { get; private set; }
    public float delayTime = 0.5f;
    void Start()
    {
        explosionTime = Time.time + delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time< explosionTime) { return; }
        Destroy(gameObject);
        
    }
}
