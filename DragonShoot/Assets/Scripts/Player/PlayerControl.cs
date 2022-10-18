using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerCharacter player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, v, 0);

        player.Move(move);
        if (Input.GetButton("Fire1"))
        {
            player.Fire();
        }
    }
}
