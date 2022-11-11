using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 player = GameObject.Find("Player").transform.position;
        if(Mathf.Abs(player.x - transform.position.x) > 5f)
        {
            ShowText(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ShowText(true);
        }

    }
        void ShowText(bool visible)
    {
        transform.Find("UI board").gameObject.SetActive(visible);
    }
}

