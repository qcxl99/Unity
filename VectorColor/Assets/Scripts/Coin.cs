using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Pickup").GetComponent<AudioSource>().Play();
        Destroy(gameObject);
        
        
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
