using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Rigidbody Ball;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Rigidbody Ball0 =  Instantiate(Ball, transform.position, transform.rotation);
        
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
