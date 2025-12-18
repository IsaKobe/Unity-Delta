using Player;
using System.Collections;
using UnityEngine;
public class WorldController : MonoBehaviour
{
    [SerializeField] Ship playerShip;
    EnemyController[] controllers;
    [SerializeField] Transform enemyControllers;
    [SerializeField] ScoreManager scoreManager;

    [SerializeField] EndScreen screen;

    [SerializeField] bool spawn;
    int progress;

    [SerializeField] int progressEnd;

    static WorldController instance;

    public static Ship Ship => instance.playerShip;
    public static ScoreManager ScoreManager => instance.scoreManager;

    private void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerShip.onEnd = LoseGame;
        if (progressEnd > 0)
            StartCoroutine(EndGame());

        Time.timeScale = 1;
        if (spawn == false) 
        {
            controllers = new EnemyController[0];
            return;
        }
        controllers = enemyControllers.GetComponentsInChildren<EnemyController>();
        foreach (var item in controllers)
        {
            //item.onEnd += Progress;
            item.enabled = true;
        }

        //progress = 0;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(progressEnd);
        WinGame();
    }

    void LoseGame(Ship ship)
    {
        StopGame();
        screen.Lose(scoreManager);
    }
    /*void Progress(EnemyController controller)
    {
        progress++;
        if(progress == controllers.Length)
        {
            WinGame();
        }
    }*/

    void WinGame()
    {
        StopGame();
        screen.Win(scoreManager);
        
    }

    void StopGame()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        foreach (var item in controllers)
        {
            item.Stop();
        }
    }
}
