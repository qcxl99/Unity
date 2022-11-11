using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    protected Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator anim;
    BossStates state;

    public bool StateIdle { get; private set; }

    Transform aim;

    public Transform fireBall;
    public Transform fireRain;
    public GameObject DeathEffect;
    public float speed = 10;
    protected float hp;
    public float maxHp = 50f;
    public float changeColorTime { get; set; }
    public float redTime = 0.1f;
    [HideInInspector]
    public bool faceRight;
    float outControlTime;
    public float LastChangeTime { get; set; }
    public bool bossFireBall { get; private set; }
    public bool state2 { get; private set; }
    public GameObject ItemFireBall;

    float speed0;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        hp = maxHp;
        state = BossStates.Idle;
        LastChangeTime = Time.time;
        pos = transform.position;
        GameObject.Find("Canvas").transform.Find("BossÑªÌõ¿ò").gameObject.SetActive(true);
        speed0 = speed;
        anim.SetBool("StateIdle", StateIdle);
    }

    // Update is called once per frame
    private void Update()
    {

        //If time is over the changecolortime, turn the character color to normal


        aim = GameObject.Find("Player").transform;
        //Boss state setting
        if (hp / maxHp < 0.5)
        {
             state2 = true;
             speed = 1.5f * speed0;
             anim.speed = 1.5f;
        }
        
            float y = rigid.velocity.y;
            switch (state)
            {
                case BossStates.Idle:
                    {
                        StateIdle = true;
                        rigid.velocity = new Vector3(0, 0, 0);
                        if (Time.time - LastChangeTime > Random.Range(3, 5))
                        {


                            int skill = Random.Range(1, 100);
                            //skill = 25;
                            if (skill < 40)
                            {
                                state = BossStates.Rush;
                                StateIdle = false;
                             }
                            else if (skill >= 40 && skill < 80)
                            {
                                state = BossStates.FireBall;
                                StateIdle = false;
                        }
                            else if (skill >= 80 && skill <= 100)
                            {
                                state = BossStates.FireRain;
                            StateIdle = false;
                        }
                        }

                        break;
                    }
                case BossStates.Rush:
                    {


                    {
                        StartCoroutine(DelayFlip(3));
                        StartCoroutine(Rush()); }
                    

                        
                        break;
                    }
                case BossStates.FireBall:
                    {

                    {
                        StartCoroutine(DelayFlip(2));

                        StartCoroutine(FireAttack(1.5f));
                    }

                        break;
                    }
                case BossStates.FireRain:
                    {


                    { StartCoroutine(FireRain(3)); }



                        break;
                    }
            }

    }
    public void SetColor(Color color)
    {
        if (renderer.color == color) { return; }
        else { renderer.color = color; }

    }
    public void GetHit(float damage)
    {

        //Change color and reduce hp when get hit
        AudioManager.Instance.HitAudio();
        anim.SetTrigger("GetHit");
        hp -= damage;
        UIManager.Instance.SetBossHp(hp, maxHp);
        Debug.Log(hp);
        //Death effect
        if (hp <= 0)
        {
            anim.SetTrigger("Dead");
            GameObject.Find("Wall (1)").SetActive(false);
            Destroy(gameObject, 1.2f);
            AudioManager.Instance.WormDeathAudio();
            Instantiate(ItemFireBall, pos, Quaternion.identity);
            
            //Instantiate(DeathEffect, transform.position, Quaternion.identity);
        }
        //Be hit effect
        /*        Vector2 force = new Vector2(transform.localPosition.x*100, 200);
                if (faceRight)
                {
                    force.x *= -1;
                }
                rigid.AddForce(force);*/
        
    }
    IEnumerator Rush()
    {
        Vector3 pos= transform.position;
        anim.SetTrigger("Rush");

        if (!faceRight)
        {
            rigid.velocity = new Vector3(-speed, 0, 0);
        }
        else
        {
            rigid.velocity = new Vector3(speed, 0, 0);
        }
            
        if (Mathf.Abs(transform.position.x - pos.x) > 30)
        {
            rigid.velocity = new Vector3(0, 0, 0);
        }
        yield return new WaitForSeconds(1.5f);
        //transform.position += Vector3.left * speed  * Time.deltaTime;

        rigid.velocity = new Vector3(0, 0, 0);
        state = BossStates.Idle;
        LastChangeTime = Time.time;
    }
    IEnumerator FireAttack(float time)
    {
        for(int i = 0; i < 3; i++)
        {
            anim.SetTrigger("BossFireBall");
            yield return new WaitForSeconds(time);
        }

        state = BossStates.Idle;
        LastChangeTime = Time.time;
    }
    IEnumerator FireRain(float time)
    {

        for (int i = 0; i < time; i++)
        {
            Flip();
            anim.SetTrigger("BossFireBall");
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(DelayFlip(1.5f));
        if (!faceRight)
        {
            rigid.velocity = new Vector3(-speed, 0, 0);
        }
        else
        {
            rigid.velocity = new Vector3(speed, 0, 0);
        }
        if (Mathf.Abs(transform.position.x - pos.x) > 25)
        {
            rigid.velocity = new Vector3(0, 0, 0);

        }
        yield return new WaitForSeconds(1);
        rigid.velocity = new Vector3(0, 0, 0);
        state = BossStates.Idle;
        
       
        LastChangeTime = Time.time;
    }

    public void CreatFireball()
    {
        if (state == BossStates.FireBall)
        {

            Transform WormFire = Instantiate(fireBall, transform.Find("WormFireBall").position, Quaternion.identity);
            AudioManager.Instance.BossFireBallAudio();
            if (faceRight)
            {
                WormFire.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed ;
                WormFire.localScale = new Vector3(1, 1, 1);

            }
            else if (!faceRight)
            {
                WormFire.localScale = new Vector3(-1, 1, 1);
                WormFire.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed ;
            }
        }
        if (state == BossStates.FireRain)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector3 aimPos = new Vector3(aim.position.x + Random.Range(-5, 5), aim.position.y + 8, 0);
                Instantiate(fireRain, aimPos, Quaternion.identity);
            }
        }

    }


    public void Flip()
    {
        Vector3 scaleRight = new Vector3(1, 1, 1);
        Vector3 scaleLeft = new Vector3(-1, 1, 1);
        if (aim.position.x - transform.position.x > 0.1f)
        {
            faceRight = true;
            transform.localScale = scaleRight;
        }
        else if (aim.position.x - transform.position.x < -0.1f)
        {
            faceRight = false;
            transform.localScale = scaleLeft;
        }
    }
    IEnumerator DelayFlip(float time)
    {
        yield return new WaitForSeconds(time);
        Flip();
    }
}
