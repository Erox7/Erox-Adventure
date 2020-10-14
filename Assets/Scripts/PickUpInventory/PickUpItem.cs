using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventManager.Instance.onPickUpItem += PickUp;
    }

    public void PickUp(Vector3Int position)
    {
        if (position.Equals(MapController.currentMap.GetComponent<Grid>().WorldToCell(transform.position)))
        {
            if (Inventory.instance.Add(item))
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.Instance.onPickUpItem -= PickUp;

    }
}
