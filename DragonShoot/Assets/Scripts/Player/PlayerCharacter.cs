using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Transform prefabBullet;
    public PlayerBorder moveBorder;
    public float speed = 3;
    public float fireCD = 0.2f;
    AudioSource audioSource;
    float lastFireTime;
    float time;
    bool fastFire;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        if (Time.time < time + 10f & fastFire ==true)
        {
            fireCD = 0.1f;
        }
        else
        {
            fireCD = 0.2f;
            fastFire = false;
        }
    }
    //Player move
    public void Move(Vector3 input)
    {
        Vector3 post = transform.position += speed * input * Time.deltaTime;
        //Limit the move area
        post.x = Mathf.Clamp(post.x, moveBorder.left, moveBorder.right);
        post.y = Mathf.Clamp(post.y, moveBorder.bottom, moveBorder.top);
        transform.position = post;
    }
    //Player shoot
    public void Fire()
    {
        
        Vector3 post = new Vector3(0, 0.2f, 0);
        if(Time.time< lastFireTime + fireCD)
        {
            return;
        }
        audioSource.Play();
        Transform bullet = Instantiate(prefabBullet, transform.position + post, Quaternion.identity);
        lastFireTime = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            time = Time.time;
            fastFire = true;
        }
    }

}
