using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using World;

[RequireComponent(typeof(UIDocument))]
public class Shop : MonoBehaviour
{
    UIDocument document;
    [SerializeField] List<ShopItem> items;
    public int ItemLength => items.Count;
        
    ShopInfo info;
    void Start()
    {
        PrepUI();
    }

    void PrepUI()
    {
        document = GetComponent<UIDocument>();
        VisualElement root = document.rootVisualElement;
        ListView view = root.Q<ListView>();

        view.makeItem = () =>
        {
            VisualElement element = new();
            element.Add(new Label() { style = { unityTextAlign = TextAnchor.MiddleLeft, maxWidth = new Length(40, LengthUnit.Percent) } });
            element.Add(new Label() { style = { unityTextAlign = TextAnchor.MiddleCenter, maxWidth = new Length(20, LengthUnit.Percent) } });
            element.Add(new Label() { style = { unityTextAlign = TextAnchor.MiddleRight } });
            //element.Add(new Label());
            return element;
        };

        view.bindItem = (el, i) =>
        {
            el.AddToClassList("item-element");
            el.style.paddingTop = StyleKeyword.Null;
            ((Label)el[0]).text = items[i].Name;
            ((Label)el[1]).text = $"{items[i].GetCost()}";
            ((Label)el[2]).text = $"{items[i].CurrentLevel} / {items[i].MaxLevel}";
            //((Label)el[3]).text = items[i].MaxLevel.ToString();
        };

        view.selectedIndicesChanged += (_) =>
        {
            info.Open(view.selectedItem as ShopItem);
        };

        view.itemsSource = items;
        info = root.Q<ShopInfo>();
        info.view = view;
        root.Q<Label>("Money-Label").SetBinding(
            nameof(SaveController.Moneys),
            nameof(Label.text),
            (ref int i) => i.ToString(),
            SaveController.GetBindingInstace);

        Button b = root.Q<Button>("Back");
        b.clicked += () => root.style.display = DisplayStyle.None;
        root.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Loads the items
    /// </summary>
    /// <param name="itemLevels">levels to assign to the items</param>
    public void LoadItems(int[] itemLevels)
    {
        List<ShopItem> tempItems = new();
        for (int i = 0; i < items.Count; i++)
        {
            ShopItem item = Instantiate(items[i]);
            item.CurrentLevel = itemLevels[i];
            item.index = i;
            tempItems.Add(item);
        }
        items = tempItems;
    }

    public void UseItems()
    {
        foreach (ShopItem item in items)
        {
            item.ModPlayer(WorldController.Ship);
        }
    }
}
