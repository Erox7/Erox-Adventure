using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Animations;

public class BlockingDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public int _neededLevers;
    public bool _opened;
    private int _activatedLevers;
    public List<int> _levers; //TODO: Si quiero que una palanca en concreto active una puerta, puedo usar el ID
    void Start()
    {
        _neededLevers = 1;
        _activatedLevers = 0;
        GlobalEventManager.Instance.onLeverActivated += ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated += DeactivateLever;
    }

    public void ActivateLever(int id)
    {
        _activatedLevers += 1;
        if (_activatedLevers >= _neededLevers)
        {
            Tilemap tilemap = MapController.currentMap.transform.Find("ForbiddenTiles").GetComponent<Tilemap>();
            Vector3Int doorPosition = MapController.currentMap.GetComponent<GridLayout>().WorldToCell(new Vector3(this.transform.position.x, this.transform.position.y, 0));
            //Borra la tile que bloquea, pero no actualiza las posiciones prohibidas
            tilemap.SetTile(doorPosition, null);
            this.GetComponent<SpriteRenderer>().enabled = false;
            GlobalEventManager.Instance.DoorOpened(doorPosition);
            _opened = true;
        }
    }
    public void DeactivateLever(int id)
    { 
        if (_activatedLevers != 0)
        {
            _activatedLevers -= 1;
        }
        if(_opened)
        {
            //CLOSE THE DOOR
            this.GetComponent<SpriteRenderer>().enabled = true;
            _opened = false;
        }

    }

    public void OnDestroy()
    {
        GlobalEventManager.Instance.onLeverActivated -= ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated -= DeactivateLever;
    }
}
