using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverTime : MonoBehaviour
{
    Transform overTime;
    public float over = 60;
    float gameTime= 0;
    void Start()
    {
        GameObject canvasTime = GameObject.Find("OverTime");
        overTime = canvasTime.transform.Find("OverTime");
        StartCoroutine(CountTime());
    }

    // Update is called once per frame
    void Update()
    {

        RestTime(gameTime);
    }
    public void RestTime(float gameT)
    {

        int time = (int)(over - gameT);
        if (time <= 0)
        {
           GameOverUI.Instance.Success();
           time = (int)over;
           gameTime = 0;
        }

        overTime.GetComponent<Text>().text = $"{time}";

    }
    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameTime++;
        }

    }
}
