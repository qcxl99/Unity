using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;
    
    [HideInInspector]
    public Vector3 move;
    Vector2 f;
    public bool Crouch { get; private set; }
    public bool jump { get; set; }
    public float throwCD = 3;
    float lastThrowTime;
    float speed;
    float jumpspeed;
    bool switchWeapon;
    void Start()
    {
        player = GetComponent<PlayerCharacter>();
        speed = player.speed;
        jumpspeed = player.jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        move = new Vector3(h, v, 0);


        /*        if (Input.GetButtonDown("Fire1"))
                {
                    player.AttackNormal();
                }*/
        //ground dash
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("MenuButton").transform.Find("UI").gameObject.SetActive(true);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            player.Grounddash();
        }
        //Dash
        if (Input.GetButtonDown("Fire2") && !player.hasDashed &&!player.isGround && !player.onWall && player.DashEnable )
        {
            if (xRaw != 0 || yRaw != 0)
                player.Dash(h, v);
        }
        //FireBall
        if (Input.GetKeyDown(KeyCode.R) && player.FireEnable)
        {

            player.FireBall();
            
        }
        //Crouch
        Crouch = Input.GetKey(KeyCode.S);
        //Jump
        jump = Input.GetButtonDown("Jump");
        
        //Switch weapon
        if (Input.GetKeyDown(KeyCode.Q) && player.ShurikenEnable)
        {
            
            switchWeapon = !switchWeapon;
            AudioManager.Instance.SwitchWeaponAudio();
        }
        //attack normal
        if (!switchWeapon)
        {
            
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(AttackNormal());
                
            }
        }
        // shuriken attack
        else
        {
            if(Time.time - lastThrowTime < throwCD)// set shuriken cd
            {
                return;
            }
            if (Input.GetButton("Fire1"))
            {
                f = player.Drag(Input.mousePosition);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                player.Throw(f);
                
                throwCD = 2f;
                lastThrowTime = Time.time;
            }
        }
        //Heal
        if (Input.GetKeyDown(KeyCode.F) && player.HealEnable && player.HealTime>0 && player.hp<player.maxHp)
        {
            StartCoroutine(Heal());
        }

        IEnumerator Heal()
        {
            player.speed /= 2;
            float jumpspeed = player.jumpSpeed;
            player.jumpSpeed = 0;
            transform.GetChild(5).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(6).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            player.Heal();
            transform.GetChild(6).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            player.speed *= 2;
            player.jumpSpeed = jumpspeed;
        }
        IEnumerator AttackNormal()
        {

            player.speed = 0;
            player.jumpSpeed = 0;
            player.AttackNormal();
            AudioManager.Instance.AttackAudio();
            yield return new WaitForSeconds(0.5f);
            player.speed = speed;
            player.jumpSpeed = jumpspeed;
        }

    }
}


