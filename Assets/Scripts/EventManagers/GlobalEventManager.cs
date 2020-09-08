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

    public event Action<Vector3Int> onPickUpItem;
    public void PickUpItem(Vector3Int position)
    {
        if (onPickUpItem != null)
        {
            onPickUpItem(position);
        }
    }

    public event Action<Item> onConsumeItem;
    public void ConsumeItem(Item item)
    {
        if (onConsumeItem != null)
        {
            onConsumeItem(item);
        }
    }

    public event Action<float> onManaDecrease;
    public void DecreaseMana(float decreaseNumber)
    {
        if (onManaDecrease != null)
        {
            onManaDecrease(decreaseNumber);
        }
    }

    public event Action<float> onManaIncrease;
    public void IncreaseMana(float increaseNumber)
    {
        if (onManaIncrease != null)
        {
            onManaIncrease(increaseNumber);
        }
    }

    public event Action<float> onHpDecrease;
    public void DecreaseHp(float decreaseNumber)
    {
        if (onHpDecrease != null)
        {
            onHpDecrease(decreaseNumber);
        }
    }

    public event Action<float> onHpIncrease;
    public void IncreaseHp(float increaseNumber)
    {
        if (onHpIncrease != null)
        {
            onHpIncrease(increaseNumber);
        }
    }
}
