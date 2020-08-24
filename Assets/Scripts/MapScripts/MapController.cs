using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapController : MonoBehaviour
{
    public static List<Vector3Int> invalidPositions = new List<Vector3Int>();
    public static Dictionary<Vector3Int,int> portals = new Dictionary<Vector3Int, int>();
    public List<Transform> itemsPosition = new List<Transform>();
    public static List<Vector3Int>  globalItemsPosition = new List<Vector3Int>();
    public static GameObject currentMap;
    public GameObject player;

    public List<GameObject> maps;

    public void Start()
    {
        ChargeNewMap(0);
        CalculateRealItemsPosition();
        GlobalEventManager.Instance.onPickUpItem += DeleteItemPosition;
        GlobalEventManager.Instance.onEnablePosition += NewValidPosition;
        GlobalEventManager.Instance.onDisablePosition += NewInValidPosition;
        GlobalEventManager.Instance.onMapChange += ChargeNewMap;
    }
    private void DeleteItemPosition(Vector3Int position)
    {
        if(globalItemsPosition.Contains(position))
        {
            globalItemsPosition.Remove(position);
        }
    }
    private void CalculateRealItemsPosition()
    {
        foreach (Transform itemPosition in itemsPosition)
        {
            Vector3Int cellPosition = currentMap.GetComponent<Grid>().WorldToCell(itemPosition.position);
            globalItemsPosition.Add(cellPosition);
        }
    }

    public void NewInValidPosition(Vector3Int doorPosition)
    {
        if (!invalidPositions.Contains(doorPosition))
        {
            invalidPositions.Add(doorPosition);
        }
    }
    public void NewValidPosition(Vector3Int doorPosition)
    {
        if (invalidPositions.Contains(doorPosition))
        {
            invalidPositions.Remove(doorPosition);
        }
    }
    public void ChargeNewMap(int mapId)
    {
        GameObject go = maps[mapId];
        if (go != null || go != default)
        {
            player.transform.position = go.GetComponent<Map>().initialCharacterPosition;
        }
        if (currentMap != null || currentMap != default)
        {
            Destroy(currentMap);
        }
        currentMap = go;
        Instantiate(currentMap);
        GlobalEventManager.Instance.MapChanged();
    }

    public void onDestroy()
    {
        GlobalEventManager.Instance.onMapChange -= ChargeNewMap;
    }

}
