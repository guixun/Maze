//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(Corridor))]
//public class CorridorInspector : Editor
//{
//    private Corridor corridor;
//    private Orient orient;
//    private Orient[] orients;
//    private int count;

//    private void OnEnable()
//    {
//        corridor = target as Corridor;
//    }

//    public override void OnInspectorGUI()
//    {
//        EditorGUILayout.BeginVertical();
//        corridor.id = EditorGUILayout.IntField("ID",corridor.id);
//        count= EditorGUILayout.IntField("Count", count);

//        //for(int i=0;i< count; i++)
//        //{
//        //    orients[i] = (Orient)EditorGUILayout.EnumPopup("Qrient"+i, orients[i]);
//        //}

//        EditorGUILayout.PropertyField(orients)
//        orient =(Orient)EditorGUILayout.EnumPopup("Qrient", orient);
//        Debug.Log(corridor.gameObject); 
//        EditorGUILayout.EndVertical();
//    }
//}
