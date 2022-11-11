using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    public float speed;
    Animator anim;
    bool explode = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!explode)
        {

            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject,0.4f);
        }
        if (transform.position.y < -20)
        {
            Destroy(gameObject); // Delete the animation prefab immediately
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            explode = true;
            Destroy(gameObject.gameObject);
        }
        
        if (UIManager.Instance.bossHpBar.fillAmount < 0.5 && CompareTag("Boss"))
        {
            
            anim.SetTrigger("Explosion1");
            AudioManager.Instance.FireExplosionAudio();
            transform.GetComponent<CinemachineImpulseSource>().GenerateImpulseWithVelocity(new Vector3(0, -0.1f, 0));
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerCharacter>().GetHit(3);
                Destroy(gameObject, 0.4f);
            }
            explode = true;
            Destroy(gameObject, 0.4f);// Delete the animation prefab after 0.4s
        }
        else
        {
            if (collision.transform.CompareTag("Player"))
            {
                explode = true;
                collision.transform.GetComponent<PlayerCharacter>().GetHit(2);
                Destroy(gameObject);
            }
            
        }


        
    }
}
