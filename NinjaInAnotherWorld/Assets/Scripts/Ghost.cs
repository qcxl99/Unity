using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
