using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            anim.SetTrigger("Get");
            if (CompareTag("FireSkill"))
            {
                GameObject.Find("FireSkill").transform.Find("Canvas").gameObject.SetActive(true);
            }
            Destroy(gameObject, 0.5f);
        }
        
    }
}
