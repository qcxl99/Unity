using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    Transform gameOverPanel;
    
    public static GameOverUI Instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        GameObject canvas = GameObject.Find("GameOver");
        gameOverPanel = canvas.transform.Find("Panel");
        gameOverPanel.gameObject.SetActive(false);
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        gameOverPanel.gameObject.SetActive(false);
    }
    public void Success()
    {
        
        Time.timeScale = 0;
        gameOverPanel.transform.Find("GameOverText").gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
        
    }
    public void PlayerDie()
    {
        Time.timeScale = 0;
        gameOverPanel.transform.Find("Win").gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);

    }
}
