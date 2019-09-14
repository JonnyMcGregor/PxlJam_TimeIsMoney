using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform)), CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        MovingPlatform example = (MovingPlatform)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newStartPosition = Handles.PositionHandle(example.StartPosition, Quaternion.identity);
        Vector3 newEndPosition = Handles.PositionHandle(example.EndPosition, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Movement Path");
            example.StartPosition = newStartPosition;
            example.EndPosition = newEndPosition;
        }
    }
}
