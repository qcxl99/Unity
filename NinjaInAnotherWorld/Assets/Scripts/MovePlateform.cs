using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlateform : MonoBehaviour {

    Rigidbody2D rigid;
    List<Vector3> pos = new List<Vector3>();
    public float moveSpeed;
    int cur = 0;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        for (int i = 3; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            pos.Add(t.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

            transform.position = Vector3.MoveTowards(transform.position, pos[cur], moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, pos[cur]) < 0.01f)
        {
            StartCoroutine(Move());
            moveSpeed = 0;
            cur++;
            
            if (cur == pos.Count)
            {
                cur = 0;
            }
        }

    }
    IEnumerator Move()
    {
        float speed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(1);
        moveSpeed = speed;
        }

    
}
