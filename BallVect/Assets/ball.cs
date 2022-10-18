using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    Rigidbody r;
    public float speed = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (z > 0)
        {
            speed = 15;
        }
        else if(z < 0)
        {
            speed = 2;
        }
        else
        {
            speed = 5;
        }
        r.velocity = new Vector3(x*8, r.velocity.y, speed);
        bool jump = Input.GetButtonDown("Jump");
        
        if (jump)
{
            Vector3 f = new Vector3(0, 6, 0);
            r.AddForce(f, ForceMode.Impulse);
            
        }
        
        
    }
}
