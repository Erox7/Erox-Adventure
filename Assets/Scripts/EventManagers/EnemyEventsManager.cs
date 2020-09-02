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
    
    public event Action<Vector2, Vector3> onTakeDamage;
    public void TakeDamage(Vector2 playerOrientation, Vector3 position)
    {
        onTakeDamage?.Invoke(playerOrientation, position);
    }

    public event Action<Vector3> onTakeProjectileDamage;
    public void TakeProjectileDamage(Vector3 position)
    {
        onTakeProjectileDamage?.Invoke(position);
    }
    
}
