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
    private Vector3 curPos;

    public Dictionary<Vector2SByte[], Orient> mazeDirt=new Dictionary<Vector2SByte[], Orient>();
    public Dictionary<Vector3, bool> book = new Dictionary<Vector3, bool>();

    private void Start()
    {
        curPos = new Vector3(0, 0 , 0);
#if Debug
        Instantiate(tilePrefab, curPos, Quaternion.identity);
#endif

        Corridor cor =Instantiate(corridors[Random.Range(0, corridors.Length)]);
        Joint joint = cor.joints[Random.Range(0, cor.joints.Length)];
        cor.transform.position = GetCorridorPos(joint);

        foreach (Vector2SByte tile in joint.tiles)
        {
            Vector3 pos = new Vector3(curPos.x + tile.x, 2, curPos.z + tile.z);
            book.Add(pos, true);
#if Debug
            Instantiate(tilePrefab, pos ,Quaternion.identity);
#endif
        }

        joint.orient = Orient.Null;


    }

    private Vector3 GetCorridorPos(Joint joint)
    {
        switch (joint.orient)
        {
            case Orient.Null:
                Debug.LogError($"[MazeBulider] wran orient{joint.orient} ,please select orient in the corridor editor");
                return Vector3.zero;
            case Orient.Right:
                return curPos + new Vector3(-8, 0, 0);
            case Orient.Left:
                return curPos + new Vector3(8, 0, 0);
            case Orient.Up:
                return curPos + new Vector3(0, 0, -8);
            case Orient.Down:
                return curPos + new Vector3(0, 0, 8);
            default:
                return Vector3.zero;
        }
    }

    //private void InitCorridors()
    //{

    //    Queue<Vector2SByte> queue = new Queue<Vector2SByte>();
    //    queue.Enqueue(Vector2SByte.Zero);
    //    while (queue != null || queue.Count > 0)
    //    {

    //    }
    //}

    public void Build()
    {

    }

    private void GetUseableCorridor(Orient orient,Vector2SByte pos)
    {
        //Corridor corridor;
        //foreach (var item in corridors)
        //{
        //    //1，方位匹配(备选块儿包含对应开口方向)
        //    item.tiles.TryGetValue(GetInvertQrient(orient), out Vector2SByte[] tiles);
        //    if (item.tiles.ContainsKey(GetInvertQrient(orient)))
        //    {
        //        //2，位置匹配(即目标位置没有被遮挡)
        //        //item.tiles.
        //    }
        //}
    }

    private void InsertCorridor(int x,int z)
    {

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

    //public List<Corridor> CreateMaze(int x, int y)
    //{

    //}
}
