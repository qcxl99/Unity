using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    Null,Red,Green,
}
public class PlayerCharacter : MonoBehaviour
{
    Rigidbody r;
    Animator anim;
    Renderer render;
    AudioSource audio;
    public float speed = 10;
    public float jumpSpeed = 4.6f;
    public int jumpCount;
    public int DeadCount;
    bool isGround;
    bool isFall;
    bool isMove;
    PlayerColor color;
    public Transform DieParticleRed;
    public Transform DieParticleGreen;
    public Transform ParticleRed;
    public Transform ParticleGreen;
    // Start is called before the first frame update
    void Start()
    {
        
        r = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<Renderer>();
        audio = GetComponent<AudioSource>();
        color = PlayerColor.Red;
        render.material.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsGround", isGround);
        anim.SetBool("IsFall", isFall);
        anim.SetBool("IsMove", isMove);
        if (color == PlayerColor.Red) 
        { 
            ParticleRed.gameObject.SetActive(true);
            ParticleGreen.gameObject.SetActive(false);
        }
        else
        {
            ParticleRed.gameObject.SetActive(false);
            ParticleGreen.gameObject.SetActive(true);
        }
           
        if (transform.position.y < -5)
        {
            isFall = true;
        }
        else isFall = false;
        if (transform.position.y < -30)
        {
            PlayerDie();
            DeadCount++;
        }
        /* float h = Input.GetAxis("Horizontal");
         float v = Input.GetAxis("Vertical");
 */
    }
    public void Move(bool jump, bool changeColor)
    {
        Vector3 vel = r.velocity;
        if (Input.GetButton("Horizontal"))
        {
            isMove = true;
            float h = Input.GetAxis("Horizontal");
            vel.z = h * speed;
        }
        else if (vel.z < 0.1) isMove = false;

        if (jump && jumpCount < 1)
        {
            isGround = false;
            vel.y = jumpSpeed;
            audio.Play();
            jumpCount++;
            
        }
        r.velocity = vel;
        if (changeColor)
        {
            ChangeColor();
        }
    }
    void ChangeColor()
    {
        if (color == PlayerColor.Red) 
        { color = PlayerColor.Green; 
          render.material.color = Color.green; }
        else
        {
            color = PlayerColor.Red;
            render.material.color = Color.red;
        }
        anim.SetTrigger("Change");
        GameObject.Find("Throw").GetComponent<AudioSource>().Play();
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag =="Red" || tag == "Green")
        {
            jumpCount = 0;
            isGround = true;
        }
   
    }*/
    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Red" || tag == "Green")
        {
            jumpCount = 0;
            isGround = true;
        }

        if (color == PlayerColor.Green && tag == "Red")
        {
            PlayerDie();
            DeadCount++;
        }
        else if (color == PlayerColor.Red && tag == "Green")
        {
            PlayerDie();
            DeadCount++;
        }
        
    }

    private void PlayerDie()
    {
        gameObject.SetActive(false);
        if(color == PlayerColor.Red)
        Instantiate(DieParticleRed, transform.position, Quaternion.identity);
        else
        Instantiate(DieParticleGreen, transform.position, Quaternion.identity);

        Invoke("GameOver", 0.3f);
    }
    void GameOver()
    {
        GameOverUI.Instance.PlayerDie();
    }
}
