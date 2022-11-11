using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMap : MonoBehaviour
{
    public bool show { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("crank-up").transform.GetChild(0).gameObject.activeSelf &&
            GameObject.Find("crank-up1").transform.GetChild(0).gameObject.activeSelf &&
            GameObject.Find("crank-up2").transform.GetChild(0).gameObject.activeSelf)

        {
            if (!show)
            {
                transform.Find("block-big1").gameObject.SetActive(true);
                show = true;
            }
            
            
        }
    }
}
