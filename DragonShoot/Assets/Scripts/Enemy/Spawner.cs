using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> monsters;

    public float minTime = 0.5f;
    public float maxTime = 2f;

    void Start()
    {
        StartCoroutine(Create());
    }
    IEnumerator Create()
    {
        while (true)
        {
            float time = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(time);
            int i1 = Random.Range(0, 4);
            int i2 = Random.Range(4, monsters.Count);
            Vector3 monsterPost1 = new Vector3(Random.Range(-2f, 2f), 5.22f, 0);
            Vector3 monsterPost2 = new Vector3(-3.2f, Random.Range(4.5f, 5.5f), 0);
            
            if (Time.time > 8f)
            {
                Instantiate(monsters[i1], monsterPost1, Quaternion.identity);
                yield return new WaitForSeconds(time * 2);
                Instantiate(monsters[i2], monsterPost2, Quaternion.identity);
                
            }
            else { Instantiate(monsters[i1], monsterPost1, Quaternion.identity); }
            
            
            
        }
    }


}
