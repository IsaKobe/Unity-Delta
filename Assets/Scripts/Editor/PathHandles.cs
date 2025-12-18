using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class PathHandles : Editor
{
    int selectedPointIndex = -1;
    EnemyController controller;
    public void OnSceneGUI()
    {
        EnemyController temp = target as EnemyController;

        if ((Event.current.type == EventType.MouseDown &&
        Event.current.button == 0 &&
        !Event.current.alt)|| controller != temp) // Allow Alt-orbiting without deselecting
        {
            // "nearestControl" checks if the mouse is hovering over any handle.
            // If it returns 0, the mouse is over empty space.
            if (HandleUtility.nearestControl == 0)
            {
                selectedPointIndex = -1; // Deselect all
                Repaint();
            }
        }

        

        for (int i = 0; i < temp.path.points.Count; i++)
        {
            if(i == selectedPointIndex) 
            {
                Handles.color = Color.red;

                EditorGUI.BeginChangeCheck();

                Vector3 pos = Handles.PositionHandle(temp.path.points[i], Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(temp, "Move Point");
                    pos.z = 0;
                    temp.path.points[i] = pos;
                    EditorUtility.SetDirty(temp.path);
                }
            }
            else
            {

                Handles.color = Color.white;

                Vector3 pos = temp.path.points[i];
                if(Handles.Button(pos, Quaternion.identity, 0.2f, 1, Handles.SphereHandleCap))
                {
                    selectedPointIndex = i;
                    Repaint();
                }
            }

            if (i > 0)
            {
                Handles.DrawLine(temp.path.points[i], temp.path.points[i - 1]);
                Handles.color = Color.green;
                Vector3 newPos = (temp.path.points[i] + temp.path.points[i - 1]) / 2;
                if (Handles.Button(newPos, Quaternion.identity, 0.1f, 0.5f, Handles.DotHandleCap))
                {
                    Undo.RecordObject(temp.path, "Point added");
                    temp.path.points.Insert(i, newPos);
                    EditorUtility.SetDirty(temp.path);
                    
                    selectedPointIndex = i;
                    Debug.Log($"add point! between {i} and {i - 1}");
                }
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            
        }
        controller = temp;
    }
}
