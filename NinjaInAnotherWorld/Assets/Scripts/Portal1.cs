using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();

            
            Time.timeScale = 0;
            Destroy(gameObject, 3);
        }
        UIManager.Instance.Complete();
        Time.timeScale = 1;
        
    }
}
