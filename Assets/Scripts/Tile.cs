using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public int x;
    public int z;
    [HideInInspector]
    public Vector3Int position;
    public Orient orient;
    public static Tile Zero { get; } = new Tile(0, 0, Orient.Null);

    
    public Tile(int x,int z,Orient orient)
    {
        this.x = x;
        this.z = z;
        this.orient = orient;
    }

    public override string ToString()
    {
        return $"(x:{x},z:{z},orient:{orient})";
    }
}
