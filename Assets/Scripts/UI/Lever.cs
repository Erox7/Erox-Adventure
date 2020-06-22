using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool _active;
    public bool _horizontal;
    public bool _id;
    public List<Sprite> sprites;
    void Start()
    {
        LoadSprite();
        EnemyEventsManager.Instance.onTakeDamage += ActivateLever;
    }

    public void ActivateLever(Vector2 rotation, Vector3 position)
    {
        Vector3Int leverPosition = MapController.currentMap.GetComponent<GridLayout>().WorldToCell(transform.position);
        if (leverPosition == position)
        {
            _active = !_active;
            LoadSprite();
        }
    }
    public void LoadSprite()
    {
        if (_horizontal)
        {
            if (_active)
            {
                TriggerLeverActivated();
                GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            else
            {
                TriggerLeverDectivated();
                GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
        }
        else
        {
            if (_active)
            {
                TriggerLeverActivated();
                GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
            else
            {
                TriggerLeverDectivated();
                GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
        }
    }
    public void TriggerLeverActivated()
    {
        GlobalEventManager.Instance.LeverActivated();
    }

    public void TriggerLeverDectivated()
    {
        GlobalEventManager.Instance.LeverDectivated();
    }
}
