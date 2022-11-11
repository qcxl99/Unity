using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected PlayerCharacter pla;
    protected Rigidbody2D rigid;
    protected SpriteRenderer renderer;
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
    public float outControlTime { get; set; }
    void Start()
    {
        pla = GetComponent<PlayerCharacter>();
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //reduce the time out of player's control
        outControlTime--;
        if (jump)
        {
            jump = true;
        }
        //the red color when get hit, then back to the normal color.
        if (Time.time < changeColorTime)
        {
            return;
        }
        SetColor(Color.white);
    }
    private void FixedUpdate()
    {
        //the time out of player's control
        if (outControlTime > 0) { return; }

        jump = false;
    }
    public void Move(Vector3 input)
    {
        float y = rigid.velocity.y;

        Flip(input.x);

        // jump setting


        if (jump && jumpCount > 0)
        {
            y = jumpSpeed;
            isGround = false;
            jump = false;
            jumpCount--;
        }
        if (isGround)
        {
            jumpCount = 1;
        }
        rigid.velocity = new Vector3(speed * input.x, y, 0);
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
        UIManager.Instance.SetPlayerHp(hp, maxHp);
        if(pla.transform.localScale.x == -1)
        {
            rigid.AddForce(Vector2.left * 100);

        }
        else if(pla.transform.localScale.x == 1)
        {
            rigid.AddForce(Vector2.right * 100);
        }
        //Death effect
        if (hp <= 0)
        {
            Destroy(gameObject);
            //Instantiate(prefabBoom, transform.position, Quaternion.identity);
        }
    }

}
