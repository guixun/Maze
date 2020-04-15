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
    private Vector3Int tilePosOffset = new Vector3Int(0, 2, 0);
#endif

    public Corridor[] corridors;
    public GameObject wallpPrefab;
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

    private void InsertWall(Orient orient)
    {
        Transform wallTrans = Instantiate(wallpPrefab).transform;
        Vector3Int pos = Vector3Int.zero;
        Vector3Int rotate = Vector3Int.zero;
        switch (orient)
        {
            case Orient.Right:
                pos = new Vector3Int(curPos.x + 2, 0, curPos.z);
                rotate = new Vector3Int(0, 270, 0);
                break;
            case Orient.Left:
                pos = new Vector3Int(curPos.x - 2, 0, curPos.z);
                rotate = new Vector3Int(0, 90, 0);
                break;
            case Orient.Up:
                pos = new Vector3Int(curPos.x, 0, curPos.z+2);
                rotate = new Vector3Int(0, 180, 0);
                break;
            case Orient.Down:
                pos = new Vector3Int(curPos.x, 0, curPos.z - 2);
                rotate = new Vector3Int(0, 0, 0);
                break;
            default:
                Debug.LogError($"[MazeBulider] wran orient{orient} ,please select orient in the corridor editor");
                break;
        }
        wallTrans.position = pos;
        wallTrans.eulerAngles = rotate;
        Debug.Log($"[MazeBulider] Put <color=red><b>\"Wall\"</b></color> in <color=red><b>{pos}</b></color>");
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
        Instantiate(tilePrefab, curPos + tilePosOffset, Quaternion.identity);
#endif

        Corridor cor = Instantiate(corridors[Random.Range(0, corridors.Length)]);
        Joint joint = cor.joints[Random.Range(0, cor.joints.Length)];
        cor.transform.position = GetCorridorPos(joint);

        Debug.Log($"[MazeBulider] Put <color=red><b>\"{cor.name}\"</b></color> in <color=red><b>{cor.transform.position}</b></color>");

        for (int i=0;i<joint.tiles.Length;i++)
        {
            Tile tile = joint.tiles[i];
            tile.position = new Vector3Int(curPos.x + tile.x, 0, curPos.z + tile.z);
            book.Add(tile.position, true);

#if Debug
            Instantiate(tilePrefab, tile.position + tilePosOffset, Quaternion.identity);
            Debug.Log($"[Debug] Position <color=green><b>{tile.position}</b></color> used");
#endif
        }

        for (int i = 0; i < joint.tiles.Length; i++)
        {
            Tile tile = joint.tiles[i];
            if (tile.orient != Orient.Null)
            {
                curPos = tile.position;
#if Debug
                Debug.Log($"[Debug] Find <color=green><b>{tile.orient}</b></color> can be used to connect other corridor");
                Debug.Log($"[Debug] CurPos is <color=green><b>{curPos.ToString()}</b></color>");
#endif
                Corridor corridor = RandomGetUseableCorridor(tile.orient);

                if(corridor!=null)
                    InsertCorridor(corridor, GetInvertQrient(tile.orient), 0);
                else
                    InsertWall(tile.orient);
            }
        }
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
                    Vector3Int pos = new Vector3Int(curPos.x + tile.x, 0, curPos.z + tile.z);
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

        if(useableIndex.Count == 0||useableIndex==null)
            Debug.Log($"[Debug] <color=red><b>Can not find any useable corridor!</color></b>");
#endif
        Corridor corridor = null;
        if (useableIndex.Count>0)
            corridor = corridors[useableIndex[Random.Range(0, useableIndex.Count)]];

        return corridor;
    }

    private void InsertCorridor(Corridor prefab, Orient orient,int depth)
    {
        if(depth>=40)
        {
            InsertWall(GetInvertQrient(orient));
            return;
        }
        Corridor cor = Instantiate(prefab);
        cor.transform.position = GetCorridorPos(orient);
        Debug.Log($"[MazeBulider] Put <color=red><b>\"{cor.name}\"</b></color> in <color=red><b>{cor.transform.position}</b></color>");
        //Debug.Log(orient);
        Tile[] tiles = cor.jointDir[orient];

        for (int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            tile.position = new Vector3Int(curPos.x + tile.x, 0, curPos.z + tile.z);
            book.Add(tile.position, true);
#if Debug
            Instantiate(tilePrefab, tile.position + tilePosOffset, Quaternion.identity);
            Debug.Log($"[Debug] Position <color=green><b>{tile.position}</b></color> used");
#endif
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            if (tile.orient != Orient.Null)
            {
                curPos = tile.position;
#if Debug
                Debug.Log($"[Debug] Find <color=green><b>{tile.orient}</b></color> can be used to connect other corridor");
                Debug.Log($"[Debug] CurPos is <color=green><b>{curPos.ToString()}</b></color>");
#endif
                Corridor corridor = RandomGetUseableCorridor(tile.orient);
                if (corridor != null)
                    InsertCorridor(corridor, GetInvertQrient(tile.orient), depth + 1);
                else
                    InsertWall(tile.orient);
            }
        }
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
