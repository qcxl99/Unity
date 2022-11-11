using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreatTrigger : MonoBehaviour
{
    public Transform bossPoint;
    public GameObject boss;
    public bool levelboss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Instantiate(boss, bossPoint.position, Quaternion.identity);
        if (!levelboss)
        {
            GameObject.Find("BGM").SetActive(false);
            GameObject.Find("BossMap/Torch1/Light 2D").SetActive(true);
            GameObject.Find("BossMap/Torch2/Light 2D").SetActive(true);}
        
        Destroy(gameObject);
    }
}
