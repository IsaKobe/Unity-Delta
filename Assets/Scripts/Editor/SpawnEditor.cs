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
    int selectedWave = 0;
    private void OnSceneGUI()
    {
        Spawner spawner = (Spawner)target;
        WaveData data = spawner.waves[0];
        for (int i = 0; i < data.path.Count; i++)
        {
            if(i != data.path.Count-1)
                Handles.DrawLine(data.path[i], data.path[i+1]);

            if(Handles.Button(data.path[i], Quaternion.identity, 0.25f, 0.25f, Handles.SphereHandleCap))
                selectedPoint = i;

            if(selectedPoint == i)
            {
                Vector2 temp = Handles.DoPositionHandle(data.path[i], Quaternion.identity);
                if (data.path[i] != temp)
                {
                    data.path[i] = temp;
                    EditorUtility.SetDirty(data);
                }
            }
        }
    }
    public override VisualElement CreateInspectorGUI()
    {
        VisualTreeAsset asset = Resources.Load<VisualTreeAsset>("SpawnerInspector");
        VisualElement element = asset.CloneTree();

        Spawner spawner = (Spawner)target;
        List<WaveData> data = spawner.waves;


        ListView waves = element.Q<ListView>("Waves");
        waves.makeItem = () => new ObjectField() { objectType = typeof(WaveData)};
        waves.bindItem = (el, i) =>
        {

            ObjectField field = el as ObjectField;
            field.value = data[i];
            field.label = data[i] != null ? data[i].name : "empty";
            field.userData = i;
            field.RegisterValueChangedCallback(WaveChange);
        };
        waves.itemsSource = data;

        return element;
    }
    void WaveChange(ChangeEvent<Object> ev)
    {
        Spawner spawner = (Spawner)target;
        int index = (int)((VisualElement)ev.target).userData;
        WaveData wave = (WaveData)ev.newValue;
        spawner.waves[index] = wave;
        EditorUtility.SetDirty(target);
    }
}
