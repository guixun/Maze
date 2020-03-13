//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Vector2SByte))]
//class Vector2SByteInspector : Editor
//{
//    Vector2SByte vector2SByte;

//    private void OnEnable()
//    {
//        vector2SByte = (Vector2SByte)(object)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        EditorGUILayout.BeginHorizontal();
//        vector2SByte.x = (sbyte)EditorGUILayout.IntField("X", 0);
//        vector2SByte.z = (sbyte)EditorGUILayout.IntField("Z", 0);
//        EditorGUILayout.LabelField("000");
//        EditorGUILayout.EndHorizontal();
//    }
//}

