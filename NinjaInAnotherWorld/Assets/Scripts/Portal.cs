using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

            UIManager.Instance.ShowWin(false);
            
            Time.timeScale = 0;
            Destroy(gameObject, 3);
        }
        
        Time.timeScale = 1;
        AudioManager.Instance.OverAudio();
    }
}
