using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 newPosition = target.position + offset;

        Vector3 position = Vector3.Lerp(transform.position, newPosition, 0.1f);
        transform.position = position;
        transform.LookAt(target);
    }
}
