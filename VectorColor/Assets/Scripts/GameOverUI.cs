using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    Transform gameOverPanel;
    
    public static GameOverUI Instance { get; private set;}
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        GameObject canvas = GameObject.Find("GameOver");
        gameOverPanel = canvas.transform.Find("GameOverPanel");
        gameOverPanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Success()
    {
        Time.timeScale = 0;
        Text t = gameOverPanel.transform.Find("GameOverText").GetComponent<Text>();
        t.text = "ÓÎÏ·Ê¤Àû";
        
        gameOverPanel.gameObject.SetActive(true);
        GameObject.Find("Fashion_SF_EDM").SetActive(false);
    }
    public void PlayerDie()
    {
        GameObject.Find("Player Hurt").GetComponent<AudioSource>().Play();
        gameOverPanel.gameObject.SetActive(true);
       
    }

}
