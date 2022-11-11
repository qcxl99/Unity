using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 宝箱 : MonoBehaviour
{
    Animator anim;
    bool open;
    void Start()
    {
        open = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            if (!open)
            {
                anim.SetTrigger("Open");
                transform.Find("Skill").gameObject.SetActive(true);
                AudioManager.Instance.ChestOpenAudio();
            }
            else { transform.Find("Skill").gameObject.SetActive(true); }
            open = true;
        }
        
    }
    public void Close()
    {
        transform.Find("Skill").gameObject.SetActive(false);
    }
}
