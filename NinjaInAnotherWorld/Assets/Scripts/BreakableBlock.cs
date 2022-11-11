using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    new SpriteRenderer renderer;
    Vector4 color;
    public Transform map;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        color = new Vector4(0.3f, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            if(renderer.color == Color.red)
            {
                AudioManager.Instance.BlockBrokenAudio();
                Destroy(gameObject);
                map.gameObject.SetActive(true);
            }
            
            renderer.color = color;
            color.x += 0.1f;
        }
    }
}
