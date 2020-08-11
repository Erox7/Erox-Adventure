using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static List<Vector3Int> invalidPositions = new List<Vector3Int>();
    public static Dictionary<Vector3Int,int> portals = new Dictionary<Vector3Int, int>();
    public static GameObject currentMap;
    public GameObject player;

    public List<GameObject> maps;

    public void Start()
    {
        ChargeNewMap(0);
        GlobalEventManager.Instance.onDoorOpened += NewValidPosition;
        GlobalEventManager.Instance.onMapChange += ChargeNewMap;
    }
    public void NewValidPosition(Vector3Int disablePosition)
    {
        if(invalidPositions.Contains(disablePosition))
        {
            invalidPositions.Remove(disablePosition);
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
