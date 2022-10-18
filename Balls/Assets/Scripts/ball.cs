using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ball : MonoBehaviour
{
    Rigidbody rigid;
    public float speed = 3;
    private bool Jump;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 input = new Vector3(h, 0, v);
        rigid.AddForce(input * speed);
        
        if (!Jump)
        {
            Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            
        }

    }
     void FixedUpdate()
    {
        if (Jump)
        {
            Vector3 f = new Vector3(0, 6, 0);
        rigid.AddForce(f, ForceMode.Impulse);

        }
        Jump = false;
    }
    }
