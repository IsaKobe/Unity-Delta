using Assets.Scripts;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Spawner))]
public class SpawnEditor : Editor
{

    int selectedPoint = -1;
    int selectedWaveIndex = 0;

    void OnEnable()
    {
        // Hides the default Move/Rotate/Scale handles
        Tools.hidden = true;
    }

    void OnDisable()
    {
        // Shows the handles again when you deselect the object
        Tools.hidden = false;
    }


    private void OnSceneGUI()
    {
        SerializedObject sO = serializedObject;
        SerializedObject wave = new SerializedObject(
            sO.FindProperty(nameof(Spawner.waves)).GetArrayElementAtIndex(selectedWaveIndex).objectReferenceValue);

        SerializedProperty points = wave.FindProperty(nameof(WaveData.path));
        for (int i = 0; i < points.arraySize; i++)
        {
            if(i != points.arraySize - 1)
                Handles.DrawLine(
                    points.GetArrayElementAtIndex(i).vector2Value, 
                    points.GetArrayElementAtIndex(i+1).vector2Value);

            if(Handles.Button(points.GetArrayElementAtIndex(i).vector2Value, Quaternion.identity, 0.25f, 0.25f, Handles.SphereHandleCap))
                selectedPoint = i;

            if(selectedPoint == i)
            {
                Vector2 temp = Handles.DoPositionHandle(points.GetArrayElementAtIndex(i).vector2Value, Quaternion.identity);
                if (points.GetArrayElementAtIndex(i).vector2Value != temp)
                {
                    points.GetArrayElementAtIndex(i).vector2Value = temp;
                    wave.ApplyModifiedProperties();
                }
            }
        }
    }
    public override VisualElement CreateInspectorGUI()
    {
        VisualTreeAsset asset = Resources.Load<VisualTreeAsset>("SpawnerInspector");
        VisualElement element = asset.CloneTree();

        SerializedObject sO = serializedObject;
        SerializedProperty waveProp = sO.FindProperty(nameof(Spawner.waves));


        RadioButtonGroup group = element.Q<RadioButtonGroup>();
        UpdateChoices(group, waveProp);
        group.value = 0;
        group.RegisterValueChangedCallback((ev) =>
        {
            selectedWaveIndex = ev.newValue;
            UpdateWaveDetails(element);
        });

        ListView waves = element.Q<ListView>("Waves");
        waves.BindProperty(waveProp);
        waves.TrackPropertyValue(waveProp, (ev) =>
        {
            UpdateChoices(group, ev);
        });

        UpdateWaveDetails(element);
        return element;
    }

    void UpdateWaveDetails(VisualElement element)
    {
        SerializedProperty waveProp = serializedObject.FindProperty(nameof(Spawner.waves));
        SerializedObject w = new(waveProp.GetArrayElementAtIndex(selectedWaveIndex).objectReferenceValue);
        SerializedProperty selectedWave = w.FindProperty(nameof(WaveData.path));
        ListView positions = element.Q<ListView>("Positions");
        positions.selectionChanged += (list) =>
        {
            selectedPoint = positions.selectedIndex;
        };
        positions.BindProperty(selectedWave);
    }

    void UpdateChoices(RadioButtonGroup group, SerializedProperty waveProp)
    {
        List<string> choices = new List<string>();
        for (int i = 0; i < waveProp.arraySize; i++)
        {
            choices.Add(waveProp.GetArrayElementAtIndex(i).objectReferenceValue.name);
        }

        group.choices = choices;
    }
}
