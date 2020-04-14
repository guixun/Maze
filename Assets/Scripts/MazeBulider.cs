using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orient:byte
{
    Null=0,
    Right,
    Left,
    Up,
    Down
}

public class MazeBulider : MonoBehaviour
{
    public int depth = 10;

#if Debug
    public GameObject tilePrefab;
#endif

    public Corridor[] corridors;
    private Vector3Int curPos;

    public Dictionary<Tile[], Orient> mazeDirt=new Dictionary<Tile[], Orient>();
    public Dictionary<Vector3Int, bool> book = new Dictionary<Vector3Int, bool>();

    private void Start()
    {
        InitCorridors();
        Build();
    }

    private Vector3Int GetCorridorPos(Joint joint)
    {
        return GetCorridorPos(joint.orient);
    }

    private Vector3Int GetCorridorPos(Orient orient)
    {
        switch (orient)
        {
            case Orient.Null:
                Debug.LogError($"[MazeBulider] wran orient{orient} ,please select orient in the corridor editor");
                return Vector3Int.zero;
            case Orient.Right:
                return curPos + new Vector3Int(-8, 0, 0);
            case Orient.Left:
                return curPos + new Vector3Int(8, 0, 0);
            case Orient.Up:
                return curPos + new Vector3Int(0, 0, -8);
            case Orient.Down:
                return curPos + new Vector3Int(0, 0, 8);
            default:
                return Vector3Int.zero;
        }
    }

    private void InitCorridors()
    {
        foreach (Corridor corridor in corridors)
        {
            corridor.Init();
        }
    }

    public void Build()
    {
        curPos = new Vector3Int(0, 0, 0);
#if Debug
        Instantiate(tilePrefab, curPos, Quaternion.identity);
#endif

        Corridor cor = Instantiate(corridors[Random.Range(0, corridors.Length)]);
        Joint joint = cor.joints[Random.Range(0, cor.joints.Length)];
        cor.transform.position = GetCorridorPos(joint);

        Debug.Log($"[MazeBulider] Put <color=red><b>\"{cor.name}\"</b></color> in <color=red><b>{cor.transform.position}</b></color>");

        for (int i=0;i<joint.tiles.Length;i++)
        {
            Tile tile = joint.tiles[i];
            Vector3Int pos = new Vector3Int(curPos.x + tile.x, 2, curPos.z + tile.z);
            book.Add(pos, true);
            tile.x = pos.x;
            tile.z = pos.z;

#if Debug
            Instantiate(tilePrefab, pos, Quaternion.identity);
            Debug.Log($"[Debug] Position <color=green><b>{pos}</b></color> used");
#endif

            if (tile.orient != Orient.Null)
            {
                curPos.x = tile.x;
                curPos.z = tile.z;
#if Debug
                Debug.Log($"[Debug] Find <color=green><b>{tile.orient}</b></color> can be used to connect other corridor");
                Debug.Log($"[Debug] CurPos is <color=green><b>{curPos.ToString()}</b></color>");
#endif
                Corridor corridor = RandomGetUseableCorridor(tile.orient);
                InsertCorridor(corridor, tile.orient);
            }
        }

        joint.orient = Orient.Null;
    }

    private Corridor RandomGetUseableCorridor(Orient orient)
    {
        List<int> useableIndex = new List<int>();
        orient = GetInvertQrient(orient);

        for (int i=0;i<corridors.Length;i++)
        {
            bool useable = true;
            if (corridors[i].jointDir.ContainsKey(orient))
            {
                Tile[] tiles = corridors[i].jointDir[orient];
                foreach (Tile tile in tiles)
                {
                    Vector3Int pos = new Vector3Int(curPos.x + tile.x, 2, curPos.z + tile.z);
                    if (book.ContainsKey(pos))
                    {
                        useable = false;
                        break;
                    }          
                }
            }
            else
                useable = false;

            if(useable)
                useableIndex.Add(i);      
        }
#if Debug
        foreach (var item in useableIndex)
            Debug.Log($"[Debug] Get useable corridor inex: <color=green><b>{item}</b></color>");
#endif

        Corridor corridor = corridors[useableIndex[Random.Range(0, useableIndex.Count)]];
        return corridor;
    }

    private void InsertCorridor(Corridor prefab, Orient orient)
    {
        Corridor cor = Instantiate(prefab);
        cor.transform.position = GetCorridorPos(GetInvertQrient(orient));
        Debug.Log($"[MazeBulider] Put <color=red><b>\"{cor.name}\"</b></color> in <color=red><b>{cor.transform.position}</b></color>");

    }

    private Orient GetInvertQrient(Orient orient)
    {
        switch (orient)
        {
            case Orient.Right:
                return Orient.Left;
            case Orient.Left:
                return Orient.Right;
            case Orient.Up:
                return Orient.Down;
            case Orient.Down:
                return Orient.Up;
        }
        Debug.LogError("[MazeBulider.GetInvertQrient] warn orient!");
        return Orient.Null;
    }
}
