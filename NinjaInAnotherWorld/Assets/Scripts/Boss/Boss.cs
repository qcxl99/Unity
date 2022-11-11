using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator anim;
    BossStates state;
    Transform aim;
    public Transform fireBall;
    public Transform terrainFire;
    public GameObject DeathEffect;
    public float speed = 10;
    protected float hp;
    public float maxHp = 100f;
    public float changeColorTime { get; set; }
    public float redTime = 0.1f;
    [HideInInspector]
    public bool faceRight;
    float outControlTime;
    public float LastChangeTime { get; set; }
    public bool bossFireBall { get; private set; }
    public bool state2 { get; private set; }
    public GameObject portal;

    float speed0;
    Vector3 pos;
    void Start()
    {
        AudioManager.Instance.DragonComeAudio();
        AudioManager.Instance.DragonBreathAudio();
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        hp = maxHp;
        state = BossStates.Idle;
        LastChangeTime = Time.time;
        pos = transform.position;
        GameObject.Find("Canvas").transform.Find("BossÑªÌõ¿ò").gameObject.SetActive(true);
        speed0 = speed;
    }

    // Update is called once per frame
    private void Update()
    {

        //If time is over the changecolortime, turn the character color to normal
        if (Time.time < changeColorTime)
        {
            return;
        }
        SetColor(Color.white);
        outControlTime--;
        if (outControlTime > 0)
        {
            anim.speed = 0;
        }
        else { anim.speed = 1; }
        aim = GameObject.Find("Player").transform;
        //Boss state setting
        if (hp / maxHp > 0.5)
        {
            float y = rigid.velocity.y;
            switch (state)
            {
                case BossStates.Idle:
                    {
                        transform.GetComponent<Collider2D>().isTrigger = false;
                        StartCoroutine(ResetPosition());
                        if (Time.time - LastChangeTime > Random.Range(3, 5))
                        {
                            
                            Debug.Log("111");
                            
                            int skill = Random.Range(1, 100);
                            //skill = 25;
                            if (skill < 40)
                            {
                                state = BossStates.Rush;
                            }
                            else if (skill >= 40 && skill < 80)
                            {
                                state = BossStates.FireBall;
                            }
                            else if (skill >= 80 && skill <= 100)
                            {
                                state = BossStates.FireRain;
                            }
                        }
                        
                        break;
                    }
                case BossStates.Rush:
                    {
                        Debug.Log("222");
                        transform.position = new Vector3(transform.position.x, pos.y - 1, pos.z);
                        rigid.velocity = new Vector3(-speed, y, 0);
                        
                        //transform.position += Vector3.left * speed  * Time.deltaTime;
                        if (transform.position.x < -50)
                        {
                            transform.position = pos + new Vector3(10, 0, 0);
                            state = BossStates.Idle;
                        }
                        
                        LastChangeTime = Time.time;
                        break;
                    }
                case BossStates.FireBall:
                    {

                        
                        StartCoroutine(FireAttack(1.5f));

                        break;
                    }
                case BossStates.FireRain:
                    {
                        transform.position = new Vector3(transform.position.x, pos.y + 6, pos.z);
                        StartCoroutine(FireRain(2));



                        break;
                    }
            }
        }
        else if(hp / maxHp < 0.5)
        {
            
            anim.SetBool("State2", state2);
            state2 = true;
            speed = 1.5f * speed0;
            anim.speed = 1.5f;
            
            switch (state)
            {
                case BossStates.Idle:
                    {
                        
                        
                        transform.GetComponent<Collider2D>().isTrigger = false;
                        StartCoroutine(ResetPosition());
                        if (Time.time - LastChangeTime > Random.Range(3, 5))
                        {

                            Debug.Log("111");

                            int skill = Random.Range(1, 100);

                            if (skill < 20)
                            {
                                state = BossStates.Rush;
                            }
                            else if (skill >= 20 && skill < 60)
                            {
                                state = BossStates.FireBall;
                            }
                            else if (skill >= 60 && skill <= 80)
                            {
                                state = BossStates.FireRain;
                            }
                            else if (skill >= 80 && skill <= 100)
                            {
                                state = BossStates.TerrainFire;
                            }
                        }
                        break;
                    }
                case BossStates.Rush:
                    {
                        transform.position = new Vector3(transform.position.x, pos.y - 1f, pos.z);
                        rigid.velocity = new Vector3(-speed, 0, 0);

                        //transform.position += Vector3.left * speed  * Time.deltaTime;
                        if (transform.position.x < -40)
                        {
                            transform.position = pos + new Vector3(10, 0, 0);
                            state = BossStates.Idle;
                        }

                        break;
                    }
                case BossStates.FireBall:
                    {

                        StartCoroutine(FireAttack(1));

                        break;
                    }
                case BossStates.FireRain:
                    {

                        transform.position = new Vector3(transform.position.x, pos.y + 8, pos.z);
                        StartCoroutine(FireRain(1.6f));

                        break;
                    }
                case BossStates.TerrainFire:
                    {
                        Debug.Log("555");
                        StartCoroutine(TerrainFire(2f));
                        break;
                    }
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
        SetColor(Color.red);
        //GameObject.Find("critical").GetComponent<AudioSource>().Play();
        changeColorTime = Time.time + redTime;
        hp -= damage;
        UIManager.Instance.SetBossHp(hp, maxHp);
        AudioManager.Instance.HitAudio();
        Debug.Log(hp);
        //Death effect
        if (hp <= 0)
        {
            anim.SetTrigger("Dead");
            
            AudioManager.Instance.DragonDeathAudio();
            Destroy(gameObject,1.2f);
            UIManager.Instance.ShowBossHp(false);
            UIManager.Instance.ShowWin(true);
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
            GameObject.Find("Wall1").SetActive(false);
            Instantiate(portal, new Vector3(71, 5.2f,0), Quaternion.identity);
        }
        //Be hit effect
        /*        Vector2 force = new Vector2(transform.localPosition.x*100, 200);
                if (faceRight)
                {
                    force.x *= -1;
                }
                rigid.AddForce(force);*/
        outControlTime = 30;
    }
    IEnumerator ResetPosition()
    {
        if (transform.position.x < -68)
        {
            transform.position = pos + new Vector3(10, 0, 0);
        }
        else if (transform.position.x > 52)
        {
            rigid.velocity = new Vector3(-speed/2, 0, 0);
        }
        if (transform.position.x == Mathf.Clamp(transform.position.x, 42f, 50f))
        {
            rigid.velocity = new Vector3(0, 0, 0);
        }
        yield return new WaitForSeconds(1f);
    }
    IEnumerator FireAttack(float time)
    {
        anim.SetTrigger("BossFireBall");
        yield return new WaitForSeconds(time);
        anim.SetTrigger("BossFireBall");
        yield return new WaitForSeconds(time);
        state = BossStates.Idle;
        LastChangeTime = Time.time ;
    }
    IEnumerator FireRain(float time)
    {
        Vector3 pos1 = transform.position;
        
        for(int i = 0; i< 3; i++)
        {
            
            anim.SetTrigger("BossFireBall");

            rigid.velocity = new Vector3(-speed, 0, 0);
            yield return new WaitForSeconds(time);
            
            if (transform.position.x < -40)
            {
                transform.position = pos + new Vector3(10, 0, 0);

            }
            
        }

        state = BossStates.Idle;
        LastChangeTime = Time.time ;
    }
    IEnumerator TerrainFire(float time)
    {

        for (int i = 0; i < 3; i++)
        {
            anim.SetTrigger("TerrainFire");
            yield return new WaitForSeconds(0.9f);
            
            yield return new WaitForSeconds(1);
        }

        state = BossStates.Idle;
        LastChangeTime = Time.time;
    }
    public void CreatFireball()
    {
        AudioManager.Instance.BossFireBallAudio();
        if(state == BossStates.FireBall)
        {

                Vector3 aimPos = new Vector3(aim.position.x + Random.Range(-2, 2), aim.position.y + 8, aim.position.z);
                Transform fire = Instantiate(fireBall, aimPos, Quaternion.identity);

        }
        if (state == BossStates.FireRain)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 aimPos = new Vector3(aim.position.x + Random.Range(-15, 15), 12, 0);
                Instantiate(fireBall, aimPos, Quaternion.identity);
            }
        }
        else { return; }

    }
    public void CreatTerrainFire()
    {
        
        if (state == BossStates.TerrainFire)
        {

            Vector3 aimPos = new Vector3(aim.position.x + Random.Range(-5, 5), 6.5f, aim.position.z);
            Transform terrainfire = Instantiate(terrainFire, aimPos, Quaternion.identity);
            Destroy(terrainfire.gameObject, 2f);

        }
        else { return; }

    }

    void Flip(float h)
    {
        Vector3 scaleRight = new Vector3(1, 1, 1);
        Vector3 scaleLeft = new Vector3(-1, 1, 1);
        if (h > 0.1f)
        {
            faceRight = true;
            transform.localScale = scaleRight;
        }
        else if (h < -0.1f)
        {
            faceRight = false;
            transform.localScale = scaleLeft;
        }
    }

}
