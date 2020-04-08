using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static List<Vector3Int> invalidPositions = new List<Vector3Int>();
    public static List<Vector3Int> portals = new List<Vector3Int>();
    public static GameObject currentMap;
    public GameObject player;

    public List<GameObject> maps;

    public void Start()
    {
        ChargeNewMap(0);
        GlobalEventManager.Instance.onMapChange += ChargeNewMap;
    }

    public void ChargeNewMap(int mapId)
    {
        GameObject go = maps[mapId];
        if (mapId == 1)
        {        
            player.transform.position = new Vector3(24, 0, 0);
        } else
        {
            player.transform.position = new Vector3(0, 0, 0);
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
