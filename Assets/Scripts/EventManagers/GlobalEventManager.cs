using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static GlobalEventManager Instance => _instance;
    private static GlobalEventManager _instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance != null || _instance != this)
        {
            Destroy(this);
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public event Action<int> onMapChange;
    public void MapChange(int id)
    {
        if (onMapChange != null)
        {
            onMapChange(id);
        }
    }

    public event Action onMapChanged;
    public void MapChanged()
    {
        if (onMapChanged != null)
        {
            onMapChanged();
        }
    }
}
