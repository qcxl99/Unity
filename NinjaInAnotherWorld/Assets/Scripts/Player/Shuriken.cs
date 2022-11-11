using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shuriken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -360), 0.15f, RotateMode.FastBeyond360).SetLoops(-1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<Boss>().GetHit(1);

            Destroy(gameObject);
            
        }
        if (collision.gameObject.layer == 7 && !collision.transform.CompareTag("Worm") && !collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<EnemyController>().GetHit(1);
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Worm"))
        {
            collision.transform.GetComponent<Worm>().GetHit(1);
            Destroy(gameObject);
        }
        transform.DOLocalRotate(transform.rotation.eulerAngles, 0.1f, RotateMode.FastBeyond360).SetLoops(-1);
        Destroy(gameObject, 1);
    }

}
