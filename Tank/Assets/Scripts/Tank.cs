using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    Rigidbody tank;
    

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        tank = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(0, 0, v);
        bool move = Input.GetButton("Fire1");
        if (!move) tank.AddForce(input * speed);
        
        
        
    }
}
