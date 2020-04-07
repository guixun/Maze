using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestCustom : MonoBehaviour
    {
        public Vector3 lookAtPoint = Vector3.zero;

        void Update()
        {
            transform.LookAt(lookAtPoint);
        }
    }
}

