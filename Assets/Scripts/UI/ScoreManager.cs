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

    static ScoreManager instance;
    int score;
    [CreateProperty]
    public int Score { get => score; set => score = value; }

    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

    private void Awake()
    {
        score = 0;
        instance = this;
        
    }

    public static void AddScore(int ammount)
    {
        instance.ManageScore(ammount);
    }

    void ManageScore(int ammount)
    {
        score += ammount;
        UIUpdate(nameof(Score));
    }
}
