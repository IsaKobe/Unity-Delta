using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Saves
{
    public class SaveControler : MonoBehaviour
    {
        [SerializeField] LevelSave testData;
#if UNITY_EDITOR

        [ContextMenu("TestSave")]
        public void TestSave()
            => Save(testData);
        [ContextMenu("TestLoad")]
        public void LoadSave()
            => Load();
#endif

        JsonSerializer jsonSerializer = new()
        {
            Formatting = Formatting.Indented,

            NullValueHandling = NullValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Error
        };

        string path = "";

        public void Save(LevelSave data)
        {
            if(path == "")
                path = Path.Join(Application.persistentDataPath, "save.json");
            using StreamWriter writer = new(path);
            JsonTextWriter jsonTextWriter = new(writer);
            jsonSerializer.Serialize(jsonTextWriter, data);
        }

        public void Load()
        {
            if (path == "")
                path = Path.Join(Application.persistentDataPath, "save.json");
            using StreamReader reader = new(path);
            JsonTextReader jsonTextReader = new(reader);
            testData = jsonSerializer.Deserialize<LevelSave>(jsonTextReader);
        }
    }
}
