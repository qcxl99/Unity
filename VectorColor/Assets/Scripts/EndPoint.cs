using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        GameOverUI.Instance.Success();
        audio.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
