using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class DungeonCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapCreator dungeonCreator = (MapCreator)target;
        if (GUILayout.Button("CreateNewDungeon"))
        {
            dungeonCreator.CreateMap();
        }

        if (GUILayout.Button("Clear all"))
        {
            dungeonCreator.DestroyAllChildren();
        }
    }
}
