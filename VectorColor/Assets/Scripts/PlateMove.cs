using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMove : MonoBehaviour
{   public int speed = 2;
    List<Vector3> poses = new List<Vector3>();
    int cur = 0;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            poses.Add(t.position);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, poses[cur], speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, poses[cur]) < 0.01f)
        {
            speed = 0;
            cur++;
            StartCoroutine(TestCoroutine());
            if (cur == poses.Count)
            {
                cur = 0;
            }
        }
    }
    void Speed()
    {
        speed = 3;
    }
    IEnumerator TestCoroutine()
    {
        speed = 0;
        yield return new WaitForSeconds(2);
        speed = 3;
    }
}
