using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickStart : MonoBehaviour
{
  public void OnBtnStart()
    {
        SceneManager.LoadScene(1);
    }
}
