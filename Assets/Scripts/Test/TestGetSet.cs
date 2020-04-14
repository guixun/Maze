using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetSet : MonoBehaviour
{
    private int A { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(A);
        A = 2;
        Debug.Log(A);
    }
}
