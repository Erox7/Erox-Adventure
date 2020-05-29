using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventsManager : MonoBehaviour
{
    public static EnemyEventsManager Instance => _instance;
    private static EnemyEventsManager _instance;

    void Awake()
    {
        if (_instance != null || _instance != this)
        {
            Destroy(this);
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }
    
    public event Action<Vector2Int,int> onTakeDamage;
    public void TakeDamage(Vector2Int playerOrientation, int position)
    {
        if (onTakeDamage != null)
        {
            onTakeDamage(playerOrientation, position);
        }
        // onTakeDamage?.Invoke(playerOrientation, position);
    }
}
