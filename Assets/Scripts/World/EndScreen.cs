using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EndScreen : MonoBehaviour
{
    UIDocument document;
    public void Lose(ScoreManager scoreManager)
    {
        PrepScreen("Defeat", scoreManager.Score);
    }

    public void Win(ScoreManager scoreManager)
    {
        PrepScreen("Victory", scoreManager.Score);
    }


    void PrepScreen(string title, int score)
    {
        gameObject.SetActive(true);
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Label>("Title").text = title;
        document.rootVisualElement.Q<Label>("TotalScore").text = score.ToString();
    }
}