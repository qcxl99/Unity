using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Destroy()
    {
        if (Time.time > time + 2.5f)
        {
            Destroy(gameObject);
        }
    }
}
