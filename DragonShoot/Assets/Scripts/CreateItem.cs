using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public Transform item;
    public float minTime = 0.5f;
    public float maxTime = 2f;
    
    void Start()
    {
        StartCoroutine(Create());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Create()
    {
        while (true)
        {
            float time = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(time);
            Vector3 post = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 3f), 0);
            Instantiate(item, post, Quaternion.identity);

        }
    }
}
