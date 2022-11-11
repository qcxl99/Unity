using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody2D rigid;
    SpriteRenderer renderer;
    protected Animator animator;
    public Transform checkGround;
    Transform hitBox;
    Transform firePosition;
    public GameObject fireBall;
    public Transform Shuriken;
    Transform [] pointPositions ;  
    public Transform points;
    public float hp;
    public float maxHp = 5;
    public float speed = 5;
    public int jumpCount = 2;
    public float jumpSpeed = 10;
    public float changeColorTime { get; set; }
    public float redTime = 0.1f;
    public bool attackNormal { get;  set; } 
    public bool isGround { get;  set; }
    public bool jump { get; set; }
    public bool faceRight { get;  set; } = true;
    public bool Crouch { get; private set; }
    public float outControlTime { get;  set; }
    public bool isDashing { get; private set; }
    public bool hasDashed { get; private set; }
    public Transform ghost;
    public int ghostNum = 3;
    public float ghostTime;
    public bool enable { get; private set; } = true;
    [Header("Skills")]
    public bool ShurikenEnable, HealEnable, DashEnable, FireBallEnable, FireEnable;

    public int HealTime { get; private set; } = 3;
    public Vector3 resetPos { get; private set; }

    public Transform checkWall;
    public bool onWall;
    bool CanMove = true;
    //public bool fireBall { get;  set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody2D>();
        hitBox = transform.Find("HitBox");
        hp = maxHp;
        DrawPoints();
        ShowPoints(false);
    }

    // Update is called once per frame
    void Update()
    {
        //reduce the time out of player's control
        outControlTime--;
        if(playerController.jump)
        {
            jump = true;
        }
        BetterJump(enable);
        //GhostTile setting
        if (isDashing && Time.time > ghostTime)
        {
            Transform ghostTile = Instantiate(ghost, transform.position, transform.rotation);
            ghostTile.localScale = transform.localScale;
            ghostTime = Time.time + 0.1f / ghostNum;
        }
        //Set the crouch status.
        if (playerController.Crouch)
        {
            Crouch = true;
        }
        //Fall
        Fall();
        //the red color when get hit, then back to the normal color.
        if (Time.time < changeColorTime)
        {
            return;
        }
        //SetColor(Color.white);

    }
    private void FixedUpdate()
    {
        CheckGround();
        CheckWall();

        //the time out of player's control
        if (outControlTime > 0)
        {
            return;
        }
        if (CanMove) { Move(playerController.move); }


        jump = false;
        Crouch = false;
    }
    public void Move(Vector3 input)
    {
        
        float y = rigid.velocity.y;
        animator.SetFloat("Speed", Mathf.Abs(input.x));
        Flip(input.x);

        // jump setting
        

       if (jump && jumpCount > 0)
        {
            y = jumpSpeed;
            AudioManager.Instance.JumpAudio();
            isGround = false;
            jump = false;
            jumpCount--;
        }
        if (isGround && !isDashing)
        {
            //wallJumped = false;
            enable = true;
        }
        if (isGround)
        {
            jumpCount = 2;
            isDashing = false;
            hasDashed = false; 
        }
        if (onWall)
        {
            
            if (!isDashing)
            {
                hasDashed = false;
            }
            input.x = 0;
            y = -2f;
            jumpCount = 1;
        }

        rigid.velocity = new Vector3(speed * input.x, y, 0);
        //Limit the move area
        //Update animation
        UpdateAnim();
    }
    //use a circle to check if the cha is on the ground.
    private void CheckGround()
    {
        /*        Ray ray = new Ray(checkGround.position, Vector3.down);
                RaycastHit hit;
                Debug.DrawLine(checkGround.position, checkGround.position - new Vector3(0, 0.15f, 0), Color.red);
                if (Physics.Raycast(ray, out hit, 0.15f, LayerMask.GetMask("Default")))
                {
                    if (hit.transform == transform)
                    {
                        return;
                    }
                    Debug.Log("isground");
                    isGround = true;
                }*/

        isGround = Physics2D.OverlapCircle(checkGround.position, 0.15f, ~LayerMask.GetMask("Player"));

    }
    void CheckWall()
    {
        onWall = Physics2D.OverlapCircle(transform.Find("CheckWall").position, 0.15f, LayerMask.GetMask("Default"));
    }
    // draw a debug circle to check the function running
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isGround)
        {
            Gizmos.color = Color.black;

        }
        if (onWall)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawSphere(checkGround.position, 0.15f);
        Gizmos.DrawSphere(transform.Find("CheckWall").position, 0.15f);
    }
    // To change the renderer's color.
    public void SetColor(Color color)
    {
        if (renderer.color == color) { return; }
        else { renderer.color = color; }

    }
    //转身动作
    void Flip(float h)
    {
        Vector3 scaleRight = new Vector3(0.726f, 0.726f, 0.726f);
        Vector3 scaleLeft = new Vector3(-0.726f, 0.726f, 0.726f);
        if (h > 0.1f)
        {
            faceRight = true;
            transform.localScale = scaleRight;
            hitBox.localRotation = new Quaternion(0,0,0,0);
        }
        else if(h < - 0.1f)
        {
            faceRight = false;
            transform.localScale = scaleLeft;
            hitBox.localRotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void AttackNormal ()
    {
      animator.SetTrigger("AttackNormal");
        
    }
    public void Dash(float x, float y)
    {
        
        transform.GetComponent<CinemachineImpulseSource>().GenerateImpulseWithVelocity(new Vector3(0, -0.8f, 0));
        AudioManager.Instance.DashAudio();
        //animator.SetTrigger("dash");
        
        rigid.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        CanMove = false;
        hasDashed = true;
        rigid.velocity += dir.normalized * 60;
        
        StartCoroutine(DashWait());
    }
    void RigidbodyDrag(float x)
    {
        rigid.drag = x;
    }
    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, 0.8f, RigidbodyDrag);

        //dashParticle.Play();
        rigid.gravityScale = 0;
        enable = false;
        //wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(0.3f);

        //dashParticle.Stop();
        rigid.gravityScale = 1.5f;
        CanMove = true;
        enable = true;
        //wallJumped = false;
        isDashing = false;
    }
    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(0.15f);  
    }

    public void FireballEnable()
    {
        FireBallEnable = true;
    }
    public void FireBall()
    {
        outControlTime = 200f;
        animator.SetTrigger("FireBall");
        AudioManager.Instance.FireballAudio();
    }
    public void CreatFireball()
    {
        firePosition = transform.Find("FirePosition");
        GameObject fire =  Instantiate(fireBall, firePosition.position, Quaternion.identity);
        AudioManager.Instance.FireballAudio();
        if (!faceRight)
        {
            fire.transform.right = Vector3.left;
        }
        
        
    }

    public void Grounddash()
    {
        animator.SetTrigger("Dash");
    }
    public void Heal()
    {

        
        if (HealTime > 0)
        {
            hp += 0.3f * maxHp;
            UIManager.Instance.SetPlayerHp(hp, maxHp);
            AudioManager.Instance.HealAudio();
            HealTime--; }
        else
        {
            HealTime = 0;
            GameObject.Find("Canvas").transform.Find("Drops").gameObject.SetActive(false);
        }
        UIManager.Instance.HealBottle(HealTime);
        if (HealTime <= 0 || !HealEnable) { return; }

        if (hp > maxHp) { hp = maxHp; }
        
    }
    public void Healenable()
    {
        HealEnable = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {   //Be hit effect of player
        if (CompareTag("Player") && collision.gameObject.layer ==7 )
        {
            //collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Boss") || collision.transform.CompareTag("Worm")
            Vector2 force = new Vector2(300, 100);
            if (faceRight)
            {
                force.x *= -1;
            }
            rigid.AddForce(force);
            outControlTime = 100;
            Debug.Log("1");
            GetHit(1);

            //collision.transform.GetComponent<Collider2D>().isTrigger = true;



        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Cherry"))
        {
            hp += 0.3f * maxHp;
            UIManager.Instance.SetPlayerHp(hp, maxHp);
            if (hp >= maxHp)
            {
                hp = maxHp;
            }
            AudioManager.Instance.GetItemAudio();
        }
        if (collision.transform.CompareTag("Shuriken"))
        {
            ShurikenEnable = true;
            Debug.Log("shuriken");
        }
        if (collision.transform.CompareTag("HealSkill"))
        {
            if (!HealEnable)
            { UIManager.Instance.HealBottle(3); }
            HealEnable = true;
            Debug.Log("heal");
        }
        if (collision.transform.CompareTag("Dash"))
        {
            DashEnable = true;
            Debug.Log("dash");
        }
        if (collision.transform.CompareTag("Drops"))
        {
            HealTime = 3;
            AudioManager.Instance.GetItemAudio();
            if (HealEnable)
            {
                UIManager.Instance.HealBottle(HealTime);
                GameObject.Find("Canvas/AddHp/Drops").gameObject.SetActive(true);
            }
        }
        if (collision.transform.CompareTag("FireSkill"))
        {
            AudioManager.Instance.FireballAudio();
            FireEnable = true;
        }
        if (collision.transform.CompareTag("Portal")) {
            animator.speed = 0; }
    }
    public void GetHit(float damage)
    {
        //Change color and reduce hp when get hit
        SetColor(Color.red);
        //GameObject.Find("critical").GetComponent<AudioSource>().Play();
        changeColorTime = Time.time + redTime;
        hp -= damage;
        UIManager.Instance.SetPlayerHp(hp, maxHp);
        AudioManager.Instance.GetHitAudio();
        //Death effect
        if (hp <= 0)
        {
            AudioManager.Instance.DeathAudio();
            Destroy(gameObject,1.5f);
            UIManager.Instance.ShowGameOver(true);
            //Instantiate(prefabBoom, transform.position, Quaternion.identity);
        }
        //Be hit animation
        animator.SetTrigger("BeHit");
        StartCoroutine(BeHitWait());
    }
    public void Shurikenenable()
    {
        ShurikenEnable = true;
    }
    public Rigidbody2D StartDrag()
    {
       
        // Creat shuriken in the presetting point
        Transform shuriken = Instantiate(Shuriken, transform.Find("ThrowPoint").position, Quaternion.identity);
        Rigidbody2D rigidShu = shuriken.GetComponent<Rigidbody2D>();
        //Set Kinematic after creating.
        rigidShu.GetComponent<Rigidbody2D>().isKinematic = true;
        
        //Every shuriken should be destroyed in 6s.
        Destroy(shuriken.gameObject, 6);
        return rigidShu;

    }
    public Vector2 Drag(Vector2 pos)
    {
        //Obtain the rigid of shuriken creating in last step
        Rigidbody2D rigidShu = Shuriken.GetComponent<Rigidbody2D>();

        //transform the format of the position.
        Vector2 throwPoint = Camera.main.ScreenToWorldPoint(pos);
        Debug.Log(pos);
        Vector2 vect = (throwPoint - (Vector2)transform.Find("ThrowPoint").position);
        float maxDist = 10;

        //Setting the vector and force
        if (vect.magnitude < 0.01f)
        {
            return throwPoint;
        }
        else if (vect.magnitude > maxDist)
        {
            vect = vect.normalized * maxDist;
        }
        Vector2 f = -vect.normalized * vect.magnitude / maxDist * 1000;
        //Calculate origin speed 
        Vector2 v0 = f * Time.fixedDeltaTime / rigidShu.mass;
        Vector2 throwOrigin = transform.Find("ThrowPoint").position;
        float t = 0;
        //Show the points in throw lines.
        for (int i = 0; i < pointPositions.Length; i++)
        {
            t += 0.2f;
            Vector2 pointPos = throwOrigin+ v0 * t + 0.5f * Physics2D.gravity * t * t;
            pointPositions[i].position = pointPos;
        }
        ShowPoints(true);
        return f;// Give a arg of force to throw.
    }
    public void Throw(Vector2 f)
    {
        //Obtain the rigid of shuriken creating in last step
        Rigidbody2D rigidShu = StartDrag();
        //Close the Kinematic
        rigidShu.isKinematic = false;
        //Lauch
        rigidShu.AddForce(f);
        ShowPoints(false);
        AudioManager.Instance.ShurikenAudio();
    }
    public void DrawPoints()
    {
        pointPositions = new Transform[20];
        for (int i = 0; i < pointPositions.Length; i++)
        {
            pointPositions[i] = Instantiate(points);
        }

    }
    public void ShowPoints(bool isShow)
    {
        foreach (var c in pointPositions)
        {
            c.gameObject.SetActive(isShow);
        }
    }
    void BetterJump(bool enable)
    {
        if (!enable)
        {
            return;
        }
        //Better jump
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity * (1.5f) * Time.deltaTime;
        }
        else if (rigid.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigid.velocity += Vector2.up * Physics2D.gravity * Time.deltaTime;
        }
    }
    private void Fall()
    {
        StartCoroutine(ResetPos());
        //Fall
        if (transform.position.y < -40)
        {
            if(resetPos.x > 54)
            {
                resetPos = new Vector3(52, 3.18f, 0);
                transform.position = resetPos;
            }
            else if(resetPos.x < 54)
            {
                resetPos = new Vector3(0, 3.18f, 0);
                transform.position = resetPos;
            }
            else if(resetPos.x > 109f)
            {
                resetPos = new Vector3(112, 2f, 0);
                transform.position = resetPos;
            }
            else if(resetPos.x > 112 && resetPos.y < -10)
            {
                resetPos = new Vector3(150, -15.2f, 0);
                transform.position = resetPos;
            }
            GetHit(0.2f * maxHp);
        }
    }
    //to record reset pos before exit the ground
    private IEnumerator ResetPos()
    {
        if (isGround) { resetPos = transform.position; }
        yield return new WaitForSeconds(6);
        if (isGround) { resetPos = transform.position; }
    }
    //受击效果加无敌时间
    IEnumerator BeHitWait()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulseWithVelocity(new Vector3(-0.6f, 0, 0));
        Time.timeScale = 0;
        transform.tag = "Enemy";
        transform.gameObject.layer = 7;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        SetColor(new Vector4(1, 1, 1, 0.4f));
        yield return new WaitForSeconds(1.9f);
        SetColor(Color.white);

        transform.tag = "Player";
        transform.gameObject.layer = 3;
    }
    void UpdateAnim()
    {

        animator.SetBool("Crouch", Crouch);
        animator.SetBool("Jump", jump);
        
        animator.SetBool("IsGround", isGround);
    }
}
