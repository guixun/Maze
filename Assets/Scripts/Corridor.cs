using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[Serializable]
public struct Joint
{
    [SerializeField]
    [Tooltip("可使用的接口")]
    public Orient orient;

    [SerializeField]
    public Vector2SByte[] tiles;

    public override string ToString()
    {
        return $"orient:{orient}";
    }
}


public class Corridor:MonoBehaviour
{
    public int id;
    public Joint[] joints;
    public Dictionary<Orient, Vector2SByte[]> jointDir = new Dictionary<Orient, Vector2SByte[]>();

    public void Init()
    {
        foreach (Joint joint in joints)
        {
            jointDir[joint.orient] = joint.tiles;
        }
    }

    private void Update()
    {
    }

    public override string ToString()
    {
        return $"ID:{id}";
    }
}
