using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour, IUpdatable
{
    public void UIUpdate(string property = "")
    {
        propertyChanged?.Invoke(this, new(property));
    }
    int score;
    [CreateProperty]
    public int Score { get => score; set => score = value; }

    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

    private void Awake()
    {
        score = 0;
    }

    public void AddScore(int ammount)
    {
        score += ammount;
        UIUpdate(nameof(Score));
    }
}
