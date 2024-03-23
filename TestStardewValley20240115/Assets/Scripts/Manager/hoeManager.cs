using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class hoeManager : MonoBehaviour
{
    public static hoeManager Instance { get; private set; }
    public Tilemap interablemap;
    public Tile tiletouming;
    public Tile chudied;
    private void Awake()
    {
        Instance = this;
        InitMap();
    }
    public void Uptohoe(Vector3 place)
    {
        print("aaa");
        Vector3Int pos = interablemap.WorldToCell(place);
        TileBase tile = interablemap.GetTile(pos);
        print(pos);
        print(tile);
        if (tile != null && tile.name == tiletouming.name)
        {
            interablemap.SetTile(pos, chudied);
        }
    }
    void InitMap()
    {
        foreach (Vector3Int t in interablemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interablemap.GetTile(t);
            if (tile != null)
            {
                print(t);
                interablemap.SetTile(t, tiletouming);
            }
        }
    }
}
