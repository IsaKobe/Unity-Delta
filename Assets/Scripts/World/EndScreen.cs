using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndScreen : MonoBehaviour
{
    UIDocument document;
    public void Lose(ScoreManager scoreManager)
    {
        PrepScreen("Defeat", scoreManager.Score);
        document.rootVisualElement.Q<Button>("Continue").style.display = DisplayStyle.None;
    }

    public void Win(ScoreManager scoreManager)
    {
        PrepScreen("Victory", scoreManager.Score);
        document.rootVisualElement.Q<Button>("Continue").clicked += () => Debug.Log("adsadsad");
    }


    void PrepScreen(string title, int score)
    {
        gameObject.SetActive(true);
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Label>("Title").text = title;
        document.rootVisualElement.Q<Label>("TotalScore").text = score.ToString();

        document.rootVisualElement.Q<Button>("Retry").clicked += () => SceneManager.LoadScene(0);
        document.rootVisualElement.Q<Button>("Exit").clicked += () => Application.Quit();
    }
}