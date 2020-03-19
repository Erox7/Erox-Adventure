using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static GlobalEventManager current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }
}
