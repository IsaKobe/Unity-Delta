using Player;
using UnityEngine;

namespace World
{
    public class WorldController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] Ship playerShip;
        [SerializeField] ScoreManager scoreManager;

        [Header("Controllers")]
        [SerializeField] Transform enemyControllers;
        [SerializeField] Transform turretControllers;
        [SerializeField] TimeController timeController;

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
        public static TimeController TimeController => instance.timeController;


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
                    ((IPausable)item).Attach(timeController);
                    item.onEnd += Progress;
                    item.enabled = true;
                    progressEnd++;
                }

            }

            turrets = turretControllers.GetComponentsInChildren<Turret>();
            foreach (var item in turrets)
            {
                ((IPausable)item).Attach(timeController);
                item.onEnd += Progress;
                progressEnd++;
            }

            ((IPausable)mapMovement).Attach(timeController);
            ((IPausable)playerShip.Input).Attach(timeController);
            timeController.StartTimer();
        }

        void LoseGame(Ship ship)
        {
            EndGame();
            screen.Lose(scoreManager);
        }

        void Progress(object controller)
        {
            progress++;
            if (progressEnd == progress)
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
                instance.timeController.StopTimer();
            else
                instance.timeController.ResumeTimer();
        }

        public static void AddScore(int score)
        {
            instance.scoreManager.AddScore(score);
        }

    }
}