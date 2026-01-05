using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LevelInfo : VisualElement
{
    Label header;
    Label score;
    Button start;
    Button close;

    Action loadCallback;
    public Action LoadCallback 
    { 
        get => loadCallback; 
        set
        {
            start.clicked -= loadCallback;
            loadCallback = value;
            start.clicked += loadCallback;
        } 
    }

    public LevelInfo()
    {
        VisualElement temp;
        Add(temp = new() { name = "header-row" });
        temp.Add(header = new("header") { name = "header"});
        temp.Add(close = new() { name = "close" });
        close.clicked += Close_clicked;

        Add(temp = new() { name = "score-collumn" });
        temp.Add(score = new("score") { name = "score" });
        temp.Add(start = new() { name = "start" });

        style.display = DisplayStyle.None;
    }


    private void Close_clicked()
    {
        style.display = DisplayStyle.None;
    }

    public void Open(int _index)
    {
        style.display = DisplayStyle.Flex;
        LevelScore _score = SaveController.GetDataForLevel(_index);
        header.text = $"Header: {_index}";
        score.text = $"Highscore: {_score.score}";
    }
}
