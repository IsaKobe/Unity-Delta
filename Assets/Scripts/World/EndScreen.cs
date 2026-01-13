using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
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
        SaveController.SaveData(scoreManager.Score, scoreManager.Score / 2);
    }


    void PrepScreen(string title, int score)
    {
        gameObject.SetActive(true);
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Label>("Title").text = title;
        document.rootVisualElement.Q<Label>("TotalScore").text = score.ToString();

        int index = SceneManager.GetActiveScene().buildIndex;

        document.rootVisualElement.Q<Button>("Retry").clicked += () => SceneManager.LoadScene(index);
        document.rootVisualElement.Q<Button>("Exit").clicked += GoToMainMenu;
    }

    public static async void GoToMainMenu()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        await SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        GameObject.Find("Main Menu").GetComponent<MainMenu>().OpenMap();// map.rootVisualElement.style.display = DisplayStyle.Flex;
        await SceneManager.UnloadSceneAsync(index);
    }
}