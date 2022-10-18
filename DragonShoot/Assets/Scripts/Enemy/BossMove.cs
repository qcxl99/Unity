using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    enum State
    {
        UpDown,
        LeftRight,
    }
    public Transform bossBullet;
    public float fireCD = 2f;
    float lastFireTime;
    public float speed1 = 3f;
    public float speed2 = 3f;
    State state;
    float i ;
    // Start is called before the first frame update
    void Start()
    {
        
        //StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        i = Random.Range(-2f, 4f);
        switch (state)
        {
            case State.UpDown:
            {
                    transform.position += Vector3.down * speed2 * Time.deltaTime;
                    if (transform.position.y < 3f)
                    {
                        state = State.LeftRight;
                    }
                    break;
            }
            case State.LeftRight:
                {
                    transform.position += Vector3.left * speed1 * Time.deltaTime;
                    if (transform.position.x < -2f)
                    {
                        speed1 = -Mathf.Abs(speed1);
                    }
                    else if (transform.position.x > 2f)
                    {
                        speed1 = Mathf.Abs(speed1);
                    }
                    Fire();
                    break;
                }
        }

    }
    public void Fire()
    {
        Vector3 post = new Vector3(0, -0.8f, 0);
        if (Time.time < lastFireTime + fireCD)
        {
            return;
        }
        Instantiate(bossBullet, transform.position + post,Quaternion.Euler(0,0,180));
        lastFireTime = Time.time;
    }

    /*    IEnumerator Move()
        {
            while (true) {
            yield return new WaitForSeconds(1);
                switch (state)
                {
                    case State.UpDown:
                        {
                            transform.position += Vector3.up * speed2 * Time.deltaTime;
                            if (transform.position.y < 3f)
                            {
                                state = State.LeftRight;
                            }
                            break;
                        }
                    case State.LeftRight:
                        {
                            transform.position += Vector3.left * speed1 * Time.deltaTime;
                            if (transform.position.x < -2f)
                            {
                                speed1 = -Mathf.Abs(speed1);
                            }
                            else if (transform.position.x > 2f)
                            {
                                speed1 = Mathf.Abs(speed1);
                            }
                            break;
                        }
                }
                if (transform.position.x < i)
            {
                transform.position += Vector3.right * speed1 * Time.deltaTime;
            }
            else { transform.position += Vector3.left * speed2 * Time.deltaTime; }
            if (transform.position.y < i)
            {
                transform.position += Vector3.up * speed1 * Time.deltaTime;
            }
            else { transform.position += Vector3.down * speed2 * Time.deltaTime; } }

        }*/
}
