using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    public UIDocument map;
    private void Awake()
    {
        VisualElement el = GetComponent<UIDocument>().rootVisualElement;
        el.Q<Button>("Map").clicked += OpenMap;//.SetActive(true);
        map.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void OpenMap()
    {
        map.rootVisualElement.style.display = DisplayStyle.Flex;
    }

}
