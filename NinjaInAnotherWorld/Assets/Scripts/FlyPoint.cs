using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPoint : MonoBehaviour
{
    Transform pla;
    new SpriteRenderer renderer;
    public float flySpeed;
    public Transform line;
    public bool isFlying { get; private set; }
    public int flyCD { get; private set; } = 3;
    public float lastFlyTime { get; private set; } = -3;
    Vector3 lineD;
    Vector3 oriPos;
    void Start()
    {
        
        line.gameObject.SetActive(false);
        pla = GameObject.Find("Player").transform;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        pla.GetComponent<Animator>().SetBool("Fly", isFlying);
        if (Time.time - lastFlyTime < flyCD) { return; }
        if (Vector2.Distance(transform.position, pla.position) <= 50 && pla.localScale.x > 0 )
        {
            renderer.color = Color.yellow;
            transform.Find("Light 2D").gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
            {
                isFlying = true;
                //flyLine =  Instantiate(line, pla.position, Quaternion.identity);
                oriPos = pla.position;
            }
            if (isFlying)
            {
                
                line.gameObject.SetActive(true);
                lineD = transform.position - pla.position;
                float angle = 180/Mathf.PI *Mathf.Asin((transform.position.y - pla.position.y)/ lineD.magnitude);
                //line.position = new Vector2(transform.position.x - pla.position.x, transform.position.y - pla.position.y);
                line.position = pla.position+ lineD/2;
                line.rotation = Quaternion.Euler(0, 0,  -90+ angle);
                line.localScale = new Vector3(line.localScale.x, lineD.magnitude, 1);
                pla.position = Vector3.MoveTowards(pla.position, transform.position, flySpeed * Time.deltaTime);
                pla.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                pla.GetComponent<PlayerCharacter>().outControlTime = 5;
                
            }
            
        }
        else
        {
            isFlying = false;
            renderer.color = Color.white;
            transform.Find("Light 2D").gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && isFlying)
        {
            renderer.color = Color.white;
            transform.Find("Light 2D").gameObject.SetActive(false);
            //pla.GetComponent<Rigidbody2D>().velocity = (Vector2.up * flySpeed);
            pla.position = Vector3.MoveTowards(pla.position, pla.position+new Vector3(0, 2, 0), flySpeed);
            pla.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            line.gameObject.SetActive(false);
            lastFlyTime = Time.time;
            isFlying = false;
        }

    }

}
