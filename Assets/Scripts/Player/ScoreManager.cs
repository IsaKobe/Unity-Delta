using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;
    [SerializeField] TMP_Text text;
    int score;
    
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
        text.text = score.ToString();
    }
}
