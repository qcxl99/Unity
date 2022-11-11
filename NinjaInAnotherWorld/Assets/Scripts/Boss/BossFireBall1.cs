using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall1 : MonoBehaviour
{
    public float speed;
    Animator anim;
    bool explode = false;

    public bool right { get; private set; }

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {

        if (!explode)
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            Destroy(gameObject);
        }
        
        if (UIManager.Instance.bossHpBar.fillAmount < 0.5)
        {
            

            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerCharacter>().GetHit(3);
                Destroy(gameObject);
            }
            Destroy(gameObject, 0.4f);// Delete the animation prefab after 0.4s
        }
        else
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerCharacter>().GetHit(2);
                Destroy(gameObject);
            }
            Destroy(gameObject); // Delete the animation prefab immediately
        }
        explode = true;


    }
}
