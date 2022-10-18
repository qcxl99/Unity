using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GM : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel2;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        panel2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Gameover()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        //Text t = panel.transform.Find("dead text").GetComponent<Text>;
        //t.text("You are dead!");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
       
    }
    public void Success()
    {
        Time.timeScale = 0;
        panel2.SetActive(true);
        //Text t = panel2.transform.Find("Text").GetComponent<Text>;
        //t.text("Win!");
    }
   
}
