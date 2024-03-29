﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController)), CanEditMultipleObjects]
public class EnemyGizmoEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        EnemyController example = (EnemyController)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newStartPosition = Handles.PositionHandle(example.point1, Quaternion.identity);
        Vector3 newEndPosition = Handles.PositionHandle(example.point2, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Enemy Path");
            example.point1 = newStartPosition;
            example.point2 = newEndPosition;
        }
    }
}
