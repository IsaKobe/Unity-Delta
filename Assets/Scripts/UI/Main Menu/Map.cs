using Assets.Scripts.UI;
using Player;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.GPUPrefixSum;

[RequireComponent(typeof(UIDocument))]
public class Map : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] int levelOffset = 1;
    public static int LevelOffset;
    [SerializeField] List<Vector2> buttonPositions;
    VisualElement map;
    LevelInfo info;

    int lastButton = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void Initialize()
    {
        LevelOffset = 0;
    }

    private void Start()
    {
        LevelOffset = levelOffset;
        Debug.Log("awake");
        VisualElement root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Root");
        map = root.Q<VisualElement>("Map");
        for (int i = 0; i < buttonPositions.Count; i++)
        {
            map.Add(new LevelButton(i, buttonPositions[i], OpenLevelInfo));
        }

        ((Button)root[1]).clicked += GoBack;
        ((Button)root[2]).clicked += OpenShop;
        info = ((LevelInfo)root[root.childCount-1]);
        info.LoadCallback = LoadGameLevel;
    }


    void OpenLevelInfo(PointerDownEvent evt)
    {
        LevelButton levelButton = (LevelButton)evt.target;
        if (levelButton.ID == lastButton)
            return;
        if (lastButton != -1)
            map.ElementAt(lastButton).RemoveFromClassList("selected");
        
        levelButton.AddToClassList("selected");
        lastButton = levelButton.ID;
        info.Open(levelButton.ID);
    }



    async void LoadGameLevel()
    {
        SaveController.SetOpenScene(lastButton);
        await SceneManager.LoadSceneAsync(lastButton + Map.LevelOffset, LoadSceneMode.Additive);
        shop.UseItems();
        await SceneManager.UnloadSceneAsync(0);
    }


    void GoBack()
    {
        GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None; //.SetActive(false);
    }
    void OpenShop()
    {
        shop.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        
    }
}
