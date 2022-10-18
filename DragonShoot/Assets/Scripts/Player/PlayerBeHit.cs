using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeHit : MonoBehaviour
{
    SpriteRenderer[] renderers;
    PlayerCharacter pla;
    public Transform prefabBoom;
    public Transform fastItem;
    public float hp = 5f;
    public float changeColorTime { get; private set; }
    

    public float redTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time< changeColorTime)
        {
            return;
        }
        SetColor(Color.white);
    }
    void SetColor(Color color)
    {
        if(renderers[0].color == color) { return; }
        foreach(var c in renderers)
        {
            c.color = color;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Item")
        {
            return;
            
        }
        else
        {       
        GameObject.Find("critical").GetComponent<AudioSource>().Play();
        SetColor(Color.red);

        changeColorTime = Time.time + redTime;
        hp -= 1;

        
        if(hp<= 0)
        {
            GameObject.Find("ch_die").GetComponent<AudioSource>().Play();
            
            Destroy(gameObject);
            GameOverUI.Instance.PlayerDie();
            Instantiate(prefabBoom, transform.position, Quaternion.identity);

        }

        }

    }
    
}
