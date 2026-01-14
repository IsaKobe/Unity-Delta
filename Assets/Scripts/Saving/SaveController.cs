using System;
using System.IO;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveController : MonoBehaviour, IUpdatable
{
    [SerializeField] int levelCount;
     Shop shop;

    [SerializeField] SaveData data;

    static SaveController instance;
    static int OpenedScene;

    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

    public void UIUpdate(string property = "")
    {
        propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(property));
    }


    public static int LevelCount => instance.levelCount;

    [CreateProperty]
    public int Moneys => data.money;

    public static int Money { get => instance.data.money; set => instance.data.money = value; }

    public static SaveController GetBindingInstace { get => instance; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void ClearInstance() 
    {
        OpenedScene = -1;
        instance = null;
    }

    public static void SetOpenScene(int scene)
    {
        OpenedScene = scene;
    }

    private void Start()
    {
        shop = GameObject.FindWithTag("Shop").GetComponent<Shop>();
        if (instance != null)
        {
            shop.LoadItems(instance.data.itemLevels);
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        string path = Path.Combine(Application.persistentDataPath, "saves");
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, "gameData.json");
        if (!File.Exists(path)) 
        {
            data = new(levelCount, shop.ItemLength);
            shop.LoadItems(data.itemLevels);
        }
        else
        {
            using StreamReader streamReader = new(path);
            data = JsonUtility.FromJson<SaveData>(streamReader.ReadToEnd());
            data.EnforceLevelLength(levelCount, shop.ItemLength);
            shop.LoadItems(data.itemLevels);
            streamReader.Close();

            using StreamWriter streamWriter = new(path);
            streamWriter.Write(JsonUtility.ToJson(data));
        }
    }

    public static void SaveData(int score, int money)
    {
        if (OpenedScene == -1)
            return;
        instance.data.SetData(OpenedScene, new(score));
        instance.data.money += money;
        instance.SaveToFile();
    }

    public static void BuyItem(int cost, int item)
    {
        instance.data.money -= cost;
        instance.UIUpdate(nameof(Moneys));
        instance.data.itemLevels[item]++;
        instance.SaveToFile();
    }

    public static LevelScore GetDataForLevel(int level)
    {
        return instance.data.GetScore(level);
    }

    void SaveToFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "saves");
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, "gameData.json");

        using StreamWriter streamWriter = new(path);
        streamWriter.Write(JsonUtility.ToJson(data));
    }

}
