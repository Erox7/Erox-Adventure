using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    public GameObject invalidPositionsGO;
    public static Dictionary<Vector3, bool> mapPositions;
    // Start is called before the first frame update
    void Start()
    {
        Tilemap collisionTileMap = invalidPositionsGO.GetComponent<Tilemap>();
        mapPositions = new Dictionary<Vector3, bool>();
        InitializeInvalidPositions(collisionTileMap);
    }
    private void InitializeInvalidPositions(Tilemap collisionTileMap)
    {
        GridLayout gl = collisionTileMap.layoutGrid;
        Vector3 origin = collisionTileMap.origin;
        Vector3 size = collisionTileMap.size;

        for (int rows = (int)origin.x; rows < (int)(origin.x + size.x); rows++)
        {
            for (int cols = (int)origin.y; cols < (int)(origin.y + size.y); cols++)
            {
                Vector3 position = new Vector3(rows, cols, 0);
                Vector3Int cellPosition = gl.WorldToCell(position);
                TileBase tb = collisionTileMap.GetTile(cellPosition);
                if (tb != null || tb != default)
                {
                    mapPositions.Add(position, true);
                } 
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
