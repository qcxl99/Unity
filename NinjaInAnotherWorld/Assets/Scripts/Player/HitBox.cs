using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    PlayerCharacter pla;
    void Start()
    {
        pla = GetComponent<PlayerCharacter>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==7 && !collision.transform.CompareTag("Worm") && !collision.transform.CompareTag("Boss"))
        {
            
            collision.transform.GetComponent<EnemyController>().GetHit(1);
            
        }
        if (collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<Boss>().GetHit(1);

        }
        if (collision.transform.CompareTag("Worm"))
        {
            collision.transform.GetComponent<Worm>().GetHit(1);
        }


    }

}
