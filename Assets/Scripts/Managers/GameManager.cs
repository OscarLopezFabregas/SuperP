using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public enum GameMode { PANIC, TOUR};

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public static bool inGame;

    public GameObject ready;
    public GameObject gameOver;

    public GameMode gameMode;

    Player player;

    public float time = 100f;

    public Text timeText = null;

    public GameObject panel;
    PanelController panelController;

    LifeManager lm;
    Fruits fruits;
    BackgroundController bc;

    Image progressBar;
    Text levelText;

    public int ballsDestroyed = 0;
    public int fruitsCaught;


    AudioSource audioSource;
    public int currentLevel = 1;

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Player>();
        lm = FindObjectOfType<LifeManager>();
        fruits = FindObjectOfType<Fruits>();
        bc = FindObjectOfType<BackgroundController>();

        if (SceneManager.GetActiveScene().name.Equals("PanicMode"))
        {
            Debug.Log("PanicModeON");
            gameMode = GameMode.PANIC;

            progressBar = GameObject.FindGameObjectWithTag("Progress").GetComponent<Image>();
            levelText = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();
        }
        else
        {
            gameMode = GameMode.TOUR;
        }
    }



    void Start()
    {
        gameOver.SetActive(false);

    
        StartCoroutine(GameStart());

        if (gameMode == GameMode.PANIC)
        {
            progressBar.fillAmount = 0;
        }

    }

    void PlayMusic()
    {
        audioSource.Play();
    }


    void Update()
    {
        if (gameMode == GameMode.TOUR)
        {
            if (BallManager.bm.balls.Count == 0
             && HexagonManager.hm.hexagons.Count == 0)
            {
                inGame = false;

                player.Win();
                lm.LifeWin();
                panel.SetActive(true);
                panelController = panel.GetComponent<PanelController>();
            }
            if (inGame && timeText != null)
            {
                time -= Time.deltaTime;
                timeText.text = "TIME: " + time.ToString("f0");
            }

        }
        else
        {

            if (BallManager.bm.balls.Count == 0
             && HexagonManager.hm.hexagons.Count == 0
             && BallSpawn.bs.free)
            {

                BallSpawn.bs.NewBall();
            }
        }

    }

    public void UpdateBallsDestroyed()
    {
        ballsDestroyed++;

        if (ballsDestroyed % UnityEngine.Random.Range(5, 20) == 0 && BallManager.bm.balls.Count > 0)
        {
            fruits.InstantiateFruit();
        }
    }

    public IEnumerator GameStart()
    {

        yield return new WaitForSeconds(2);

        ready.SetActive(false);
        if (gameMode == GameMode.TOUR)
        {
            BallManager.bm.StartGame();
            HexagonManager.hm.StartGame();
            if (audioSource != null)
            {
                audioSource.Play();
            }

        }
        else
        {
            BallSpawn.bs.NewBall();
        }


        GameManager.inGame = true;
    }

    public void GameOver()
    {
        StartCoroutine(GameIsOver());
    }

    public int AleatoryNumber()
    {
        return UnityEngine.Random.Range(0, 3);
    }

    public void PanicProgress()
    {
        if (gameMode == GameMode.PANIC)
        {
            progressBar.fillAmount += 0.1f;
            if (progressBar.fillAmount == 1)
            {
                progressBar.fillAmount = 0;
                currentLevel++;
                BallSpawn.bs.IncreaseDifficulty();
                bc.BackgroundChange();
                if (currentLevel < 10)
                {
                    levelText.text = "Lv.0" + currentLevel.ToString();
                }
                else
                {
                    levelText.text = "Lv." + currentLevel.ToString();
                }
            }
        }
    }

    public void NextLevel(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator GameIsOver()
    {
        gameOver.SetActive(true);
        if (ScoreManager.sm.currentScore >= GameStatus.gs.gameHighScore && ScoreManager.sm.currentScore != 0)
        {
            ScoreManager.sm.highScore = ScoreManager.sm.currentScore;
            GameStatus.gs.gameHighScore = ScoreManager.sm.highScore;
            // Debug.Log("current score > highscore");
            GameStatus.gs.GuardarHighScore(ScoreManager.sm.highScore);
            Social.ReportScore(ScoreManager.sm.highScore, "CgkItby8-JQLEAIQAQ", (bool success) => { });
        }
        yield return new WaitForSeconds(1.5f);


        EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
        SceneManager.LoadScene("Map");
    }



}
