using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Vector2SByte
{
    public sbyte x;
    public sbyte z;
    public Orient orient;
    public static Vector2SByte Zero { get; } = new Vector2SByte(0, 0, Orient.Null);

    public sbyte this[int index]
    {
        get
        {
            sbyte result;
            if (index != 0)
            {
                if (index != 1)
                {
                    throw new IndexOutOfRangeException(string.Format("Invalid Vector2SByte index addressed: {0}!", index));
                }
                result = z;
            }
            else
            {
                result = x;
            }
            return result;
        }
        set
        {
            if (index != 0)
            {
                if (index != 1)
                {
                    throw new IndexOutOfRangeException(string.Format("Invalid Vector2SByte index addressed: {0}!", index));
                }
                z = value;
            }
            else
            {
                x = value;
            }
        }
    }
    public Vector2SByte(sbyte x,sbyte z,Orient orient)
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
