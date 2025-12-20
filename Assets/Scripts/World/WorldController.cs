using Player;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Progress;
public class WorldController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Ship playerShip;
    [SerializeField] ScoreManager scoreManager;

    [Header("Controllers")]
    [SerializeField] Transform enemyControllers;
    [SerializeField] Transform turretControllers;

    EnemyController[] enemies;
    Turret[] turrets;

    [Header("UI")]
    [SerializeField] EndScreen screen;
    [SerializeField] MapMovement mapMovement;
    [SerializeField] bool spawn;
    [SerializeField] int progress;
    [SerializeField] int progressEnd;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Domain reload", "UDR0001:Domain Reload Analyzer", Justification = "<fixed in awake>")]
    static WorldController instance;

    public static Ship Ship => instance.playerShip;
    public static ScoreManager ScoreManager => instance.scoreManager;

    Action onPause;
    Action onResume;



    private void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerShip.onEnd = LoseGame;

        progressEnd = 0;
        enemies = enemyControllers.GetComponentsInChildren<EnemyController>();
        if (spawn) 
        {
            foreach (var item in enemies)
            {
                ((IPausable)item).Attach(ref onPause, ref onResume);
                item.onEnd += Progress;
                item.enabled = true;
                progressEnd++;
            }
        
        }

        turrets = turretControllers.GetComponentsInChildren<Turret>();
        foreach(var item in turrets)
        {
            ((IPausable)item).Attach(ref onPause, ref onResume);
            item.onEnd += Progress;
            progressEnd++;
        }

        ((IPausable)mapMovement).Attach(ref onPause, ref onResume);
        Time.timeScale = 1;
    }

    void LoseGame(Ship ship)
    {
        EndGame();
        screen.Lose(scoreManager);
    }

    void Progress(object controller)
    {
        progress++;
        if(progressEnd == progress)
        {
            EndGame();
            screen.Win(scoreManager);
        }
    }

    void EndGame()
    {
        StopAllCoroutines();
        foreach (var item in enemies)
        {
            item.Stop();
        }
        foreach (var turret in turrets)
        {
            turret.Deactivate();
        }
    }

    public static void ToggleGame(bool pause)
    {
        if (pause)
            instance.onPause?.Invoke();
        else
            instance.onResume?.Invoke();
    }

    public static void AddScore(int score)
    {
        instance.scoreManager.AddScore(score);
    }

}
