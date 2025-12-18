using Player;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelInterface : MonoBehaviour
{
    UIDocument document;

    private void Start()
    {
        document = GetComponent<UIDocument>();

        Label label = document.rootVisualElement.Q<Label>("Health");
        label.SetBinding(nameof(Ship.Health), nameof(Label.text), (ref float h) => h.ToString(), WorldController.Ship);

        label = document.rootVisualElement.Q<Label>("Score");
        label.SetBinding(nameof(ScoreManager.Score), nameof(Label.text), (ref int s) => s.ToString(), WorldController.ScoreManager);
    }
}
