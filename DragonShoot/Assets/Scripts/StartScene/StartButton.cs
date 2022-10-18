using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void OnBtnStart()
    {
        StartCoroutine(Click());
    }
    IEnumerator Click()
    {
        GameObject btn = GameObject.Find("StartButton");
        GameObject.Find("alche").GetComponent<AudioSource>().Play();
        for(int i = 0; i<2; i++)
        {
            btn.SetActive(!btn.activeInHierarchy);
            yield return new WaitForSeconds(0.12f);
        }
        GameObject.Find("clear_screen").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
   
}
