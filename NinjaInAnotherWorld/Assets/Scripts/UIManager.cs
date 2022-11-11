using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get;private set; }
    public Image playerHpBar;
    public Image bossHpBar;
    public Image playerHpBarEffect;
    public Image bossHpBarEffect;
    public bool showHp;
    public Transform canvas;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (showHp)
        {
            if (playerHpBarEffect.fillAmount > playerHpBar.fillAmount)
            {
                playerHpBarEffect.fillAmount -= 0.0005f;
            }
            else
            {
                playerHpBarEffect.fillAmount = playerHpBar.fillAmount;
            }
            if (bossHpBarEffect.fillAmount > bossHpBar.fillAmount)
            {
                bossHpBarEffect.fillAmount -= 0.0005f;
            }
            else
            {
                bossHpBarEffect.fillAmount = bossHpBar.fillAmount;
            }
        }

    }
    public void SetPlayerHp(float hp, float maxHp)
    {
        playerHpBar.fillAmount = hp / maxHp;
    }
    public void SetBossHp(float hp, float maxHp)
    {
        bossHpBar.fillAmount = hp / maxHp;
    }
    public void ShowGameOver(bool isShow)
    {
        Time.timeScale = 0;
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        AudioManager.Instance.GameOverAudio();
        canvas.Find("GameOver").gameObject.SetActive(isShow);
        
    }
    public void ShowWin(bool isShow)
    {
        
        canvas.Find("YOUWIN").gameObject.SetActive(isShow);
    }
    public void Complete()
    {
        canvas.transform.Find("Win").gameObject.SetActive(true);
        StartCoroutine(Achievement());
    }
    public void ShowBossHp(bool isShow)
    {
        canvas.Find("BossÑªÌõ¿ò").gameObject.SetActive(isShow);
    }
    public void HealBottle(float time)
    {
        Transform AddHp = canvas.Find("AddHp");
        AddHp.gameObject.SetActive(true);
        canvas.Find("UIBoard").gameObject.SetActive(true);
        
        AddHp.Find("RestTime").GetComponent<TextMeshProUGUI>().text =$"{time}";
        
        if (time == 0) { AddHp.Find("Drops").gameObject.SetActive(false); }
        StartCoroutine(ShowHealBottle());
    }
    IEnumerator ShowHealBottle()
    {
        yield return new WaitForSeconds(1.5f);
        canvas.Find("AddHp").gameObject.SetActive(false);
        canvas.Find("UIBoard").gameObject.SetActive(false);
    }
    public void RestartLevel1()
    {
        StartCoroutine(WaitLoad(1));
        Time.timeScale = 1;
    }
    public void RestartBoss()
    {
        StartCoroutine(WaitLoad(2));
        Time.timeScale = 1;
    }
    public void StartScene()
    {
        StartCoroutine(WaitLoad(0));
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Debug.Log("eee");
        Application.Quit();
    }
    IEnumerator WaitLoad(int level)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }
    IEnumerator Achievement()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.GetItemAudio();
        canvas.transform.Find("Win/Achievement").gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.transform.Find("Win/Achievement").gameObject.SetActive(false);
        AudioManager.Instance.OverAudio();
    }
    public void Pause()
    {
        GameObject.Find("Player").GetComponent<Animator>().speed = 0;
        

    }
    public void Play()
    {
        GameObject.Find("Player").GetComponent<Animator>().speed = 1;
        
    }
}
