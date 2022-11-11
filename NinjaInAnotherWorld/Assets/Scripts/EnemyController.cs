using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected PlayerCharacter pla;
    protected Rigidbody2D rigid;
    new SpriteRenderer renderer;
    protected Animator animator;
    public Transform checkGround;
    protected float hp;
    public float maxHp = 5;
    public float speed = 5;
    public int jumpCount = 1;
    public float jumpSpeed = 10;
    public float changeColorTime { get; set; }
    public float redTime = 0.1f;
    public bool isGround { get; set; }
    public bool jump { get; set; }
    public bool faceRight { get; set; } = true;
    bool canMove;
    public float outControlTime { get; set; }
    public float borderPos1 =10;
    public float borderPos2 = 5;
    public float inputX;
    public float inputY;
    Vector2 originPos;
    float speedBuffer;
    void Start()
    {
        originPos = transform.position;
        pla = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        hp = maxHp;
        StartCoroutine(AIJump());
        speedBuffer= speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        //reduce the time out of control
        outControlTime--;

        //the red color when get hit, then back to the normal color.
        if (Time.time < changeColorTime)
        {
            return;
        }
        SetColor(Color.white);
        if (outControlTime > 0)
        {
            animator.speed = 0.3f;
            speed = speedBuffer / 3;
            return;
        }
        speed = speedBuffer;
        if (Mathf.Abs( Vector2.Distance(originPos, pla.transform.position)) <= 10)
        {
            canMove = false;
            if (transform.position.x - pla.transform.position.x > 0.1f)
            {
                
                
                if (CompareTag("Rat"))
                {
                    Flip(-0.15f);
                    rigid.velocity = Vector3.left * speed ;
                    //inputX = -1;
                }
                else
                {
                    Flip(-0.15f);
                    transform.position = (Vector2.MoveTowards(transform.position, pla.transform.position, speed * Time.deltaTime));
                }
            }
            else if (transform.position.x - pla.transform.position.x < -0.1f) {

                if (CompareTag("Rat"))
                {
                    Flip(0.15f);
                    rigid.velocity = Vector3.right * speed ;
                    //inputX = 1;
                }
                else
                {
                    Flip(0.15f);
                    transform.position = (Vector2.MoveTowards(transform.position, pla.transform.position, speed * Time.deltaTime));
                }
            }
            else
            {
                canMove = true;
            }

           
        }
        else
        {
            canMove = true;;
/*            StartCoroutine(HorizontalMove());
            StartCoroutine(VerticalMove());
            ;*/
        }

        if (transform.position.x-originPos.x >= borderPos1)
        {

            inputX = -1f;
        }
        if (transform.position.x - originPos.x <= -borderPos1)
        {

            inputX = 1f;
        }
        if (transform.position.y - originPos.y <= -borderPos2)
        {

            inputY = 1f;
        }
        if (transform.position.y - originPos.y >= borderPos2)
        {

            inputY = -1f;
        }

        if (jump)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        //the time out of player's control
        if (outControlTime > 0 ||!canMove ) { 
            animator.speed = 0.3f;
            speed = speedBuffer/ 3;
            return;
        }
        speed = speedBuffer;
        Move(new Vector2(inputX,inputY));
        jump = false;
    }
/*    //use a circle to check if the cha is on the ground.
    protected void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, 0.15f, ~LayerMask.GetMask("Enemy"));
    }*/
    IEnumerator HorizontalMove()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        inputX = Random.Range(-1f, 1f);


    }
    IEnumerator VerticalMove()
    {
        if (CompareTag("Eagle"))
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));
            inputY = Random.Range(-0.8f, 1f);

        }
        else
        {
            yield break;
        }


    }
    IEnumerator AIJump()
    {
        if (CompareTag("Frog"))
        {
            yield return new WaitForSeconds(2);
            int chance = Random.Range(0, 100);
                if (chance < 30)
                {
                    jump = true;
            }
        }
        else
        {
            yield break;
        }
        
    }

    public void Move(Vector3 input)
    {
        float y = rigid.velocity.y;

        Flip(input.x);

        // jump setting


        if (CompareTag("Frog"))
        {
            if (jump)
            {
                
                y = jumpSpeed;
                isGround = false;

                jump = false;
            }
            rigid.velocity = new Vector3(speed * input.x, y, 0);
        }
        else
        {
            rigid.velocity = new Vector3(speed * input.x, speed * input.y, 0);
        }


        

        //Limit the move area
        //Update animation
    }

    public void SetColor(Color color)
    {
        if (renderer.color == color) { return; }
        else { renderer.color = color; }

    }

    //转身动作
    void Flip(float h)
    {
        Vector3 scaleRight = new Vector3(-1f, 1f, 1f);
        Vector3 scaleLeft = new Vector3(1f, 1f, 1f);
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
    public void GetHit(float damage)
    {
        //Change color and reduce hp when get hit
        SetColor(Color.red);
        //GameObject.Find("critical").GetComponent<AudioSource>().Play();
        changeColorTime = Time.time + redTime;
        hp -= damage;
        AudioManager.Instance.HitAudio();
        //Set a force from attack's direction
        if (pla.transform.localScale.x < 0)
            {
            if (CompareTag("Rat"))
            {
                rigid.AddForce(Vector2.left * 1000);
            }
            else
            {
                rigid.AddForce(Vector2.left * 250);
            }   

            }
            else if (pla.transform.localScale.x > 0)
            {
            if (CompareTag("Rat"))
            {
                rigid.AddForce(Vector2.right * 1000);
            }
            else
            {
                rigid.AddForce(Vector2.right * 250);
            }
        }
        
        //Death effect
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject,0.8f);
            //Instantiate(prefabBoom, transform.position, Quaternion.identity);
        }
    }

}
