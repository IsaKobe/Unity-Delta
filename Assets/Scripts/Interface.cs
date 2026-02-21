using Assets.Scripts.Player;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Interface : MonoBehaviour
{
    UIDocument doc;
    VisualElement root;
    ProgressBar healthBar;

    [SerializeField] Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;
        healthBar = root.Q<ProgressBar>("Health");

        healthBar.dataSource = player;
        DataBinding binding = new DataBinding() 
        { 
            dataSourcePath = new(nameof(Player.Health)),
            bindingMode = BindingMode.ToTarget
        };
        healthBar.SetBinding(new(nameof(ProgressBar.value)), binding);

        /*healthBar.SetBinding(nameof(Player.Health), nameof(ProgressBar.value), player);
        healthBar.SetBinding(nameof(Player.Health), nameof(ProgressBar.title), (ref float h) => $"{h} / {player.MaxHealth}");*/
    }
}
