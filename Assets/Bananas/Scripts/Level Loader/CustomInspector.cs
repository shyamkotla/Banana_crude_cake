using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelCreator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelCreator lvcreator = (LevelCreator)target;
        if(GUILayout.Button("Create Level object"))
        {
            lvcreator.CreateLevelInfo();
        }
    
    }
}