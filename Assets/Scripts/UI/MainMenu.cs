using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument document;
    private void Awake()
    {
        Button button = document.rootVisualElement.Q<Button>("Level");
        button.clicked += Button_clicked;
    }

    private void Button_clicked()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync("Level");
    }
}
