using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory instance");
            return;
        }
        instance = this;
    }
    #endregion 

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space;
    public float actualMana;
    public float maxMana;
    public float minMana;
    public List<Item> items = new List<Item>();

    private float manaPotionIncrease = 3f;
    public bool Add(Item item)
    {
        if (!item.isConsumable)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            items.Add(item);
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        else if (item.isConsumable)
        {
            ConsumeItem(item);
        }
        return true;
    }
    public void ConsumeItem(Item item)
    {
        if(item.itemName.Equals("ManaPotion"))
        {
            if((actualMana + manaPotionIncrease) > 10)
            {
                GlobalEventManager.Instance.IncreaseMana(10 - actualMana);
                actualMana += (10 - actualMana);
            } else
            {
                actualMana += manaPotionIncrease;
                GlobalEventManager.Instance.IncreaseMana(manaPotionIncrease);
            }
        } else
        {
            //GlobalEventManager.Instance.ConsumeItem(item);
        }
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool ContainsItemName(string itemName)
    {
        foreach(Item item in items)
        {
            if(item.itemName.Equals(itemName))
            {
                return true;
            }
        }
        return false;
    }
}
