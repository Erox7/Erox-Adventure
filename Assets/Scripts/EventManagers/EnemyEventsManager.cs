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

    public event Action<Vector3Int,float, Vector2> onMakeDamage;
    public void MakeDamage(Vector3Int position, float damage, Vector2 orientation)
    {
        onMakeDamage?.Invoke(position, damage, orientation);
    }

    public event Action<Vector3Int, float> onMakeProjectileDamage;
    public void MakeProjectileDamage(Vector3Int position, float damage)
    {
        onMakeProjectileDamage?.Invoke(position, damage);
    }
}
