﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int id;
    public Vector3 initialCharacterPosition;
    public GameObject invalidPositionsGO;
    public GameObject portalPositionsGO;
    // Start is called before the first frame update
    void Start()
    {
        MapController.currentMap = gameObject;
        Tilemap collisionTileMap = invalidPositionsGO.GetComponent<Tilemap>();
        InitializeInvalidPositions(collisionTileMap);
        Tilemap portalTileMap = portalPositionsGO.GetComponent<Tilemap>();
        InitializePortals(portalTileMap);
    }

    private void InitializePortals(Tilemap portalTileMap)
    {
        GridLayout gl = portalTileMap.layoutGrid;
        Vector3 origin = portalTileMap.origin;
        Vector3 size = portalTileMap.size;

        for (int rows = (int)origin.x; rows < (int)(origin.x + size.x); rows++)
        {
            for (int cols = (int)origin.y; cols < (int)(origin.y + size.y); cols++)
            {
                Vector3 position = new Vector3(rows, cols, 0);
                Vector3Int cellPosition = gl.WorldToCell(position);
                TileBase tb = portalTileMap.GetTile(cellPosition);
                if ((tb != null || tb != default) && !MapController.portals.ContainsKey(cellPosition))
                {
                    MapController.portals.Add(cellPosition, Int32.Parse(tb.name));
                }
            }
        }
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
                if ((tb != null || tb != default) && !MapController.invalidPositions.Contains(cellPosition))
                {
                    MapController.invalidPositions.Add(cellPosition);
                } 
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
