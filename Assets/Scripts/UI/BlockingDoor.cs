using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class BlockingDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public int _neededLeversNum;
    private int _activatedLeversNum;
    public bool _opened;
    public List<int> _neededLevers = new List<int>(); //TODO: Si quiero que una palanca en concreto active una puerta, puedo usar el ID
    public List<int> _activatedLevers = new List<int>(); //TODO: Si quiero que una palanca en concreto active una puerta, puedo usar el ID
    void Start()
    {
        _neededLeversNum = 1;
        _activatedLeversNum = 0;
        GlobalEventManager.Instance.onLeverActivated += ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated += DeactivateLever;
    }

    public void ActivateLeverNum(int id)
    {
        _activatedLeversNum += 1;
        if (_activatedLeversNum >= _neededLeversNum)
        {
            Tilemap tilemap = MapController.currentMap.transform.Find("ForbiddenTiles").GetComponent<Tilemap>();
            Vector3Int doorPosition = MapController.currentMap.GetComponent<GridLayout>().WorldToCell(new Vector3(this.transform.position.x, this.transform.position.y, 0));
            tilemap.SetTile(doorPosition, null);
            this.GetComponent<SpriteRenderer>().enabled = false;
            GlobalEventManager.Instance.EnablePosition(doorPosition);
            _opened = true;
        }
    }
    public void ActivateLever(int id)
    {

        if (!_activatedLevers.Contains(id))
        {
            _activatedLevers.Add(id);
            if(allNeededLeversActivated())
            {
                Tilemap tilemap = MapController.currentMap.transform.Find("ForbiddenTiles").GetComponent<Tilemap>();
                Vector3Int enablePosition = MapController.currentMap.GetComponent<GridLayout>().WorldToCell(new Vector3(transform.position.x, transform.position.y, 0));
                tilemap.SetTile(enablePosition, null);
                GlobalEventManager.Instance.EnablePosition(enablePosition);
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    public void DeactivateLeverNum(int id)
    { 
        if (_activatedLeversNum != 0)
        {
            _activatedLeversNum -= 1;
        }
        if(_opened)
        {
            //CLOSE THE DOOR
            this.GetComponent<SpriteRenderer>().enabled = true;
            _opened = false;
        }

    }
    public void DeactivateLever(int id)
    {
        if (_activatedLevers.Contains(id))
        {
            _activatedLevers.Remove(id);
        }
        if (allNeededLeversActivated())
        {
            //REESCRIBIR LA TILE?
            Vector3Int disablePosition = MapController.currentMap.GetComponent<GridLayout>().WorldToCell(new Vector3(transform.position.x, transform.position.y, 0));
            GlobalEventManager.Instance.DisablePosition(disablePosition);
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

    }
    public bool allNeededLeversActivated()
    {
        return _activatedLevers.AsQueryable().Intersect(_neededLevers).Count() == _activatedLevers.Count();
    }
    public void OnDestroy()
    {
        GlobalEventManager.Instance.onLeverActivated -= ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated -= DeactivateLever;
    }
}
