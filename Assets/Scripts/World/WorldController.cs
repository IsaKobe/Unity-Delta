using Player;
using UnityEngine;
public class WorldController : MonoBehaviour
{
    [SerializeField] Ship player;
    [SerializeField] EnemyController[] controllers;
    [SerializeField] ScoreManager scoreManager;

    [SerializeField] EndScreen screen;

    int progress;

    public Ship Player => player;
    public ScoreManager ScoreManager => scoreManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.onEnd = LoseGame;
        foreach (var item in controllers)
        {
            item.onEnd += Progress;
        }
        progress = 0;
    }

    void LoseGame(Ship ship)
    {
        screen.Lose(scoreManager);
        foreach (var item in controllers)
        {
            item.Stop();
        }
    }


    void Progress(EnemyController controller)
    {
        progress++;
        if(progress == controllers.Length)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        screen.Win(scoreManager);
        foreach (var item in controllers)
        {
            item.Stop();
        }
    }
}
