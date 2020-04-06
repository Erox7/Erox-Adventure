using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static List<Vector3Int> invalidPositions = new List<Vector3Int>();
    public static List<Vector3Int> portals = new List<Vector3Int>();

    public List<GameObject> maps;

    public void Start()
    {
        GlobalEventManager.Instance.onMapChange += ChargeNewMap;
    }

    public void ChargeNewMap(int mapId)
    {
        GameObject go = maps[mapId];
        Instantiate(go, go.transform.position, go.transform.rotation);
    }

    public void onDestroy()
    {
        GlobalEventManager.Instance.onMapChange -= ChargeNewMap;
    }

}
