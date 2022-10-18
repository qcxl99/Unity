using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeHit : MonoBehaviour
{
    SpriteRenderer[] renderers;
    
    public Transform prefabBoom;
    
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
        GameObject.Find("critical").GetComponent<AudioSource>().Play();
        SetColor(Color.red);

        changeColorTime = Time.time + redTime;
        hp -= 1;

        
        if(hp<= 0)
        {
            GameObject.Find("hit_rush").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            Instantiate(prefabBoom, transform.position, Quaternion.identity);

        }
    }
}
