using Player;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelInterface : MonoBehaviour
{
    [SerializeField] WorldController controller;
    UIDocument document;

    private void Start()
    {
        ScoreManager manager = controller.ScoreManager;
        Ship ship = controller.Player;
        document = GetComponent<UIDocument>();

        Label label = document.rootVisualElement.Q<Label>("Health");
        label.SetBinding(nameof(Ship.Health), nameof(Label.text), (ref float h) => h.ToString(), ship);

        label = document.rootVisualElement.Q<Label>("Score");
        label.SetBinding(nameof(ScoreManager.Score), nameof(Label.text), (ref int s) => s.ToString(), manager);
    }
}
