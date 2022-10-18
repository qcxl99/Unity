using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{
    public float speedright = 3f;
    public float speeddown = 3f;
    float i ;
    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(-0.38f, 3.21f);
    }

    // Update is called once per frame
    void Update()
    {


        //Delete the enemy object beyond the bottom of border.
        if (transform.position.x < i)
        {
            transform.position += Vector3.right * speedright * Time.deltaTime;
        }
        else { transform.position += Vector3.down * speeddown * Time.deltaTime; }
    }

}
