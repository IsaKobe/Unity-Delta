using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarInterface : MonoBehaviour
{
    [SerializeField] Player player;
    private void Awake()
    {
        UIDocument document = GetComponent<UIDocument>();
        Slider healthSlider = document.rootVisualElement.Q<Slider>();

        healthSlider.dataSource = player;
        DataBinding binding = new DataBinding()
        {
            dataSourcePath = new(nameof(Player.Health)),
            bindingMode = BindingMode.ToTarget
        };
        healthSlider.SetBinding(new(nameof(Player.Health)), binding);
    }
}
