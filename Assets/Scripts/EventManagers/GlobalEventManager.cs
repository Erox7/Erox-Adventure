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

    public event Action<int> onLeverActivated;
    public void LeverActivated(int id)
    {
        if (onLeverActivated != null)
        {
            onLeverActivated(id);
        }
    }

    public event Action<int> onLeverDectivated;
    public void LeverDectivated(int id)
    {
        if (onLeverDectivated != null)
        {
            onLeverDectivated(id);
        }
    }

    public event Action<Vector3Int> onEnablePosition;
    public void EnablePosition(Vector3Int position)
    {
        if (onEnablePosition != null)
        {
            onEnablePosition(position);
        }
    }

    public event Action<Vector3Int> onDisablePosition;
    public void DisablePosition(Vector3Int position)
    {
        if (onDisablePosition != null)
        {
            onDisablePosition(position);
        }
    }
}
