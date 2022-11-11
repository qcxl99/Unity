using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackup : MonoBehaviour
{
    new SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            renderer.sprite = null;
            AudioManager.Instance.CrankDownAudio();
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
