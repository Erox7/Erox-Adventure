using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOutOfCamera : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
