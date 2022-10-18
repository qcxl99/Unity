using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Transform pitch;
    public Rigidbody prefabBullet;
    public Transform cannonBody;
    public Transform Bulletpost;

    public float hSpeed = 20;
    public float vSpeed = 20;
    public float speed = 50;
    // Start is called before the first frame update
    void Start()
    {
        pitch = transform.Find("俯仰");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Rotate(0, h * hSpeed * Time.deltaTime, 0);
        bool move = Input.GetButton("Fire1");
        if (move)  pitch.Rotate(v * vSpeed * Time.deltaTime, 0, 0);

        bool fire = Input.GetButtonDown("Jump");
        if (fire)
        {
            Rigidbody bullet = Instantiate(prefabBullet, Bulletpost.position, Bulletpost.rotation);
            bullet.velocity = cannonBody.up * speed;
        }
    }
}
