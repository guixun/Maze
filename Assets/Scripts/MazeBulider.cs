using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orient:byte
{
    Null,
    Right,
    Left,
    Up,
    Down
}

public class MazeBulider : MonoBehaviour
{
    public int depth = 10;
    //所有待选的走廊
    private List<Corridor> corridors;
    public GameObject corridor_I;
    public GameObject corridor_L;

    public Dictionary<Vector2SByte[], Orient> mazeDirt=new Dictionary<Vector2SByte[], Orient>();
    public bool[,] book = new bool[sbyte.MaxValue, sbyte.MaxValue];

    private void Start()
    {
        
        //InitCorridors();
    }

    private void InitCorridors()
    {
        corridors = new List<Corridor>();
        //1
        Corridor tmp = new Corridor();
        tmp.tiles.Add(Orient.Right, new Vector2SByte[]
        {
            new Vector2SByte(-4,0,Orient.Right),
            new Vector2SByte(-8,0,Orient.Null),
            new Vector2SByte(-12,0,Orient.Left)
        });

        tmp.tiles.Add(Orient.Left, new Vector2SByte[]
        {
            new Vector2SByte(4,0,Orient.Left),
            new Vector2SByte(8,0,Orient.Null),
            new Vector2SByte(12,0,Orient.Right)
        });
        tmp.gameObject = corridor_I;
        corridors.Add(tmp);

        //2
        tmp = new Corridor();
        tmp.tiles.Add(Orient.Up, new Vector2SByte[]
        {
            new Vector2SByte(0,-4,Orient.Up),
            new Vector2SByte(0,-8,Orient.Null),
            new Vector2SByte(0,-12,Orient.Down)
        });

        tmp.tiles.Add(Orient.Down, new Vector2SByte[]
        {
            new Vector2SByte(0,4,Orient.Down),
            new Vector2SByte(0,8,Orient.Null),
            new Vector2SByte(0,12,Orient.Up)
        });
        tmp.gameObject = corridor_I;
        tmp.gameObject.transform.Rotate(new Vector3(0, 90, 0));
        corridors.Add(tmp);

        //4
        tmp = new Corridor();
        tmp.tiles.Add(Orient.Left, new Vector2SByte[]
        {
            new Vector2SByte(4,0,Orient.Left),
            new Vector2SByte(8,0,Orient.Null),
            new Vector2SByte(8,-4,Orient.Down)
        });
        tmp.tiles.Add(Orient.Down, new Vector2SByte[]
        {
            new Vector2SByte(0,4,Orient.Down),
            new Vector2SByte(0,8,Orient.Null),
            new Vector2SByte(-4,8,Orient.Left)
        });

        tmp.gameObject = corridor_L;
        tmp.gameObject.transform.Rotate(new Vector3(0, 90, 0));
        corridors.Add(tmp);

        //4
        tmp = new Corridor();
        tmp.tiles.Add(Orient.Left, new Vector2SByte[]
        {
            new Vector2SByte(4,0,Orient.Left),
            new Vector2SByte(8,0,Orient.Null),
            new Vector2SByte(8,-4,Orient.Down)
        });
        tmp.tiles.Add(Orient.Down, new Vector2SByte[]
        {
            new Vector2SByte(0,4,Orient.Down),
            new Vector2SByte(0,8,Orient.Null),
            new Vector2SByte(-4,8,Orient.Left)
        });

        tmp.gameObject = corridor_L;
        tmp.gameObject.transform.Rotate(new Vector3(0, 90, 0));
        corridors.Add(tmp);

        Queue<Vector2SByte> queue = new Queue<Vector2SByte>();
        queue.Enqueue(Vector2SByte.Zero);
        while(queue!=null||queue.Count>0)
        {

        }
    }

    private void GetUsableCorridor(Orient orient,Vector2SByte pos)
    {
        Corridor corridor;
        foreach (var item in corridors)
        {
            //1，方位匹配(备选块儿包含对应开口方向)
            item.tiles.TryGetValue(GetInvertQrient(orient), out Vector2SByte[] tiles);
            if (item.tiles.ContainsKey(GetInvertQrient(orient)))
            {
                //2，位置匹配(即目标位置没有被遮挡)
                //item.tiles.
            }
        }
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
