using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarInterface : MonoBehaviour
{
    [SerializeField] Player player;
    private void Awake()
    {
        UIDocument document = GetComponent<UIDocument>();
        ProgressBar healthBar = document.rootVisualElement.Q<ProgressBar>();


        healthBar.highValue = player.MaxHealth;
        healthBar.value = player.Health;

        healthBar.dataSource = player;
        DataBinding binding = new DataBinding()
        {
            dataSourcePath = new(nameof(Player.Health)),
            bindingMode = BindingMode.ToTarget
        };
        healthBar.SetBinding(new(nameof(ProgressBar.value)), binding);
    }
}
