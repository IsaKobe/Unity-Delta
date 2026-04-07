using Assets.Scripts;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    ListView positionList;
    public int index;
    int selectedPoint;


    public override VisualElement CreateInspectorGUI()
    {
        Spawner spawner = (Spawner)target;
        
        VisualTreeAsset m_InspectorUXML = Resources.Load<VisualTreeAsset>("Editor/Spawner");
        VisualElement element = m_InspectorUXML.CloneTree();
        #region 
        ListView view = element.Q<ListView>("Waves");

        SerializedProperty waves = serializedObject.FindProperty(nameof(Spawner.waves));
        view.makeItem = () =>
        {/*
            VisualElement e = view.itemTemplate.CloneTree();
            return e;*/
            VisualElement e = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    paddingLeft = 3,
                    paddingRight = 3,
                    justifyContent = Justify.SpaceBetween
                }
            };
            e.Add(new ObjectField()
            {
                objectType = typeof(WaveData),
            });
            return e;
        };
        view.bindItem = (e, i) =>
        {
            ObjectField field = e.Q<ObjectField>();
            field.BindProperty(waves.GetArrayElementAtIndex(i));
            //field.userData = i;
            //field.RegisterValueChangedCallback(Change);
        };
        view.unbindItem = (e, i) =>
        {
            e.Q<ObjectField>().Unbind();
            //e.Q<ObjectField>().UnregisterValueChangedCallback(Change);
        };
        view.onAdd = (e) =>
        {
            waves.InsertArrayElementAtIndex(waves.arraySize);
            serializedObject.ApplyModifiedProperties();
        };
        view.onRemove = (e) =>
        {

            int i = e.selectedIndex;
            if (i == -1 && i > spawner.waves.Count)
                i = spawner.waves.Count-1;
            waves.DeleteArrayElementAtIndex(i);
            serializedObject.ApplyModifiedProperties();
        };
        view.BindProperty(waves);

        positionList = element.Q<ListView>("Positions");
        positionList.makeItem = () =>
        {
            Vector2Field field = new();
            return field;
        };

        positionList.bindItem = (e, i) =>
        {
            Vector2Field field = e.Q<Vector2Field>();
            field.value = spawner.waves[index].path[i];
            field.name = i.ToString();
            field.userData = i;
            field.RegisterValueChangedCallback(Change);
        };
        positionList.unbindItem = (e, i) =>
        {
            e.Q<Vector2Field>().RegisterValueChangedCallback(Change);
        };

        positionList.onAdd = (e) =>
        {
            spawner.waves[index].path.Add(new());
            EditorUtility.SetDirty(spawner.waves[index]);
            positionList.RefreshItems();
        };

        positionList.onRemove = (e) =>
        {
            if (e.selectedIndex > -1 && e.selectedIndex < spawner.waves[index].path.Count)
                e.selectedIndex = spawner.waves[index].path.Count - 1;
            spawner.waves[index].path.RemoveAt(e.selectedIndex);
            EditorUtility.SetDirty(spawner.waves[index]);
            positionList.RefreshItems();
        };
        positionList.selectionChanged += (e) =>
        {
            selectedPoint = positionList.selectedIndex;
        };


        RadioButtonGroup group = element.Q<RadioButtonGroup>();
        group.choices = spawner.waves.Select(q => q != null ? q.name : "empty");
        group.RegisterValueChangedCallback((e) => SetPositionView(e.newValue));
        if (spawner.waves.Count > 0)
        {
            group.value = 0;
            SetPositionView(0);
        }
        #endregion
        return element;
    }

    void OnSceneGUI()
    {
        Spawner spawner = (Spawner)target;

        SerializedObject wave = new SerializedObject(
            serializedObject.FindProperty(nameof(Spawner.waves))
            .GetArrayElementAtIndex(index)
            .objectReferenceValue as WaveData);

        SerializedProperty path = wave.FindProperty(nameof(WaveData.path));
        for (int i = 0; i < path.arraySize; i++)
        {
            SerializedProperty position = path.GetArrayElementAtIndex(i);
            if (i == 0)
                Handles.color = Color.green;
            else if (i == path.arraySize - 1)
                Handles.color = Color.red;

            if (Handles.Button(position.vector2Value, Quaternion.identity, 0.4f, 0.4f, Handles.SphereHandleCap))
                selectedPoint = i;
            Handles.color = Color.white;

            if (i == selectedPoint)
            {
                Vector2 v = Handles.DoPositionHandle(position.vector2Value, Quaternion.identity);
                if (v != position.vector2Value)
                {
                    position.vector2Value = v;
                    wave.ApplyModifiedProperties();
                    positionList.RefreshItem(i);
                }
            }
            if (i < path.arraySize - 1)
                Handles.DrawLine(position.vector2Value, path.GetArrayElementAtIndex(i+1).vector2Value);
        }
        /*WaveData data = spawner.waves[index];
        for (int i = 0; i < data.path.Count; i++)
        {
            if (i == 0)
                Handles.color = Color.green;
            else if (i == data.path.Count - 1)
                Handles.color = Color.red;

            if (Handles.Button(data.path[i], Quaternion.identity, 0.4f, 0.4f, Handles.SphereHandleCap))
                selectedPoint = i;
            Handles.color = Color.white;

            if (i == selectedPoint)
            {
                Vector2 v = Handles.DoPositionHandle(data.path[i], Quaternion.identity);
                if (v != data.path[i])
                {
                    data.path[i] = v;
                    EditorUtility.SetDirty(data);
                    positionList.RefreshItem(i);
                }
            }
            if (i < data.path.Count - 1)
                Handles.DrawLine(data.path[i], data.path[i + 1]);
        }*/
    }
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

    void SetPositionView(int _index)
    {
        index = _index;
        //positionList.BindProperty(new SerializedObject(target));
        positionList.itemsSource = ((Spawner)target).waves[index].path;
    }

    /*void Change(FocusOutEvent ev)
    {
        Vector2Field el = (ev.currentTarget) as Vector2Field;
        Spawner spawner = (Spawner)target;
        int i = (int)el.userData;
        spawner.waves[index].path[i] = el.value;
        EditorUtility.SetDirty(spawner.waves[index]);
    }*/

    void Change(ChangeEvent<Vector2> vec)
    {
        Spawner spawner = (Spawner)target;
        int i = (int)((VisualElement)vec.target).userData;
        spawner.waves[index].path[i] = vec.newValue;
        EditorUtility.SetDirty(spawner.waves[index]);
    }

    void Change(ChangeEvent<Object> wave)
    {
        Spawner spawner = (Spawner)target;
        int i = (int)((VisualElement)wave.target).userData;
        spawner.waves[index] = wave.newValue as WaveData;
        EditorUtility.SetDirty(spawner);
    }
}
