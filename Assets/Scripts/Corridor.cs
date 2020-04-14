using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[Serializable]
public class Joint
{
    [SerializeField]
    [Tooltip("可使用的接口")]
    public Orient orient;
    public Vector3Int position;
    [SerializeField]
    public Tile[] tiles;

    public override string ToString()
    {
        return $"orient:{orient}";
    }
}


public class Corridor:MonoBehaviour
{
    public int id;
    public Joint[] joints;
    public Dictionary<Orient, Tile[]> jointDir = new Dictionary<Orient, Tile[]>();

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
