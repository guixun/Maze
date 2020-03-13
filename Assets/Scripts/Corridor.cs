using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor
{
    public GameObject gameObject;
    public Dictionary<Orient, Vector2SByte[]> tiles = new Dictionary<Orient, Vector2SByte[]>();
}
