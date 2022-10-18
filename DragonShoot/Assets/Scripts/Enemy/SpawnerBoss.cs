using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    public List<Transform> monsters;

    public float minTime = 10f;
    public float maxTime = 20f;
   
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
            int i = Random.Range(0, monsters.Count);
            
            Vector3 monsterPost1 = new Vector3(Random.Range(-2f, 2f), 5.22f, 0);
            GameObject.Find("boss_come").GetComponent<AudioSource>().Play();
            Instantiate(monsters[i], monsterPost1, Quaternion.identity);  
        }
    }


}
