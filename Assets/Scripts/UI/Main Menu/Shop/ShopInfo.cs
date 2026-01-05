using UnityEngine.UIElements;

[UxmlElement]
public partial class ShopInfo : VisualElement
{
    public ListView view;
    readonly VisualElement infoElem;
    readonly VisualElement noInfo;

    readonly Label add;
    readonly VisualElement value;

    readonly Label title;
    readonly Label changeTitle;
    readonly Label original;
    readonly Label newValue;
    readonly Label paragraph;
    readonly Label cost;

    readonly VisualElement image;
    readonly Button buy;

    ShopItem item;

    public ShopInfo() : base() 
    {
        AddToClassList("menu");
        Add(infoElem = new() { style = { flexGrow = 1 } });

        VisualElement i;
        infoElem.Add(i = new VisualElement() { style = { flexGrow = 1 } });
        i.Add(title = new Label("Title") { name = "Item-Header" });

        VisualElement info;
        i.Add(info = new VisualElement() { name = "Info" });
        info.Add(image = new VisualElement() { name = "Item-Image" });

        VisualElement change;
        info.Add(change = new VisualElement() { name = "Change" });
        change.Add(changeTitle = new Label("Label") { name = "Change-Title" });
        change.Add(add = new Label("Add") { name = "Change-Add", style = { display = DisplayStyle.None} });

        change.Add(value = new VisualElement() { name = "Change-Value" });
        value.Add(original = new Label("Val") { name = "Change-Original", style = { left = 0} });
        value.Add(new VisualElement() { name = "Arrow" });
        value.Add(newValue = new Label("New") { name = "Change-New", style = { right = 0 } });

        info.Add(paragraph = new Label("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer pulvinar auctor nunc, et ornare arcu. Proin feugiat purus nec nulla tincidunt feugiat. Proin pretium est nisi, id rhoncus nunc commodo et. Morbi dolor lectus, elementum vel tempor in, dignissim at ligula. Morbi non dolor ultricies, dapibus dolor vel, rhoncus nisl. Suspendisse") { name = "Paragraph" });

        VisualElement Cost;
        infoElem.Add(Cost = new VisualElement() { name = "Cost" });
        VisualElement tmp;
        Cost.Add(tmp = new() { name = "Cost-Row" });
        tmp.Add(new Label("Cost: "));
        tmp.Add(cost = new Label() { name = "Cost" });
        Cost.Add(buy = new Button() { name = "Buy"});
        buy.AddToClassList("test");
        buy.text = "Buy";

        Add(noInfo = new Label("Select an item.") { style = { display = DisplayStyle.Flex} });
        infoElem.style.display = DisplayStyle.None;
    }

    public void Open(ShopItem _item)
    {
        infoElem.style.display = DisplayStyle.Flex;
        noInfo.style.display = DisplayStyle.None;

        item = _item;

        title.text = _item.Name;

        value.style.display = DisplayStyle.Flex;
        add.style.display = DisplayStyle.None;

        original.text = _item.Original();
        newValue.text = _item.NewValue();

        changeTitle.text = _item.Name;
        image.style.backgroundImage = _item.image;

        paragraph.text = _item.paragraph;
        cost.text = _item.GetCost().ToString();

        buy.clicked -= Buy_clicked;
        if (item.GetCost() <= SaveController.Money)
        {
            buy.enabledSelf = true;
            buy.clicked += Buy_clicked;
        }
        else
            buy.enabledSelf = false;
    }

    void Buy_clicked()
    {
        if (item.GetCost() <= SaveController.Money)
            item.IncreaseLevel();
        view.RefreshItems();
        Open(item);
    }
}