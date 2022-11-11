using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyFire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

    }
    IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<Boss>().GetHit(2);

            Destroy(gameObject);

        }
        if (collision.gameObject.layer == 7 && !collision.transform.CompareTag("Worm") && !collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<EnemyController>().GetHit(2);
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Worm"))
        {
            collision.transform.GetComponent<Worm>().GetHit(2);
            Destroy(gameObject);
        }
    }
}
