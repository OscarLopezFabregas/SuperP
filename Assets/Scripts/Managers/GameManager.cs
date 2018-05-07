using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public enum SaveType
{
    HIGHSCORE,
    LIFES

};

public enum GameMode { PANIC, TOUR};

public class GameManager : MonoBehaviour {
    
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

    static string rutaArchivo;

    AudioSource audioSource;
    public int currentLevel = 1;
       
    private void Awake()
    {
        rutaArchivo = Application.dataPath + "/datos.dat";
        audioSource = GetComponent<AudioSource>();
        if (gm==null)
        {
            gm = this;
        }
        else if (gm!=this)
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Player>();
        lm = FindObjectOfType<LifeManager>();
        fruits = FindObjectOfType<Fruits>();
        bc = FindObjectOfType<BackgroundController>();

        if(SceneManager.GetActiveScene().name.Equals("PanicMode"))
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
    
    
	
	void Start ()
    {
        gameOver.SetActive(false);

        Cargar();
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


    void Update ()
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
            if (inGame && timeText!=null)
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

        if(ballsDestroyed % UnityEngine.Random.Range(5,20)==0 && BallManager.bm.balls.Count >0)
        {
            fruits.InstantiateFruit();
        }
    }
   
    public IEnumerator GameStart()
    {
        
        yield return new WaitForSeconds(2);
          
        ready.SetActive(false);
        if(gameMode == GameMode.TOUR)
        {
            BallManager.bm.StartGame();
            HexagonManager.hm.StartGame();
            if(audioSource != null)
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
        if(gameMode == GameMode.PANIC)
        {
            progressBar.fillAmount += 0.1f;
            if(progressBar.fillAmount == 1)
            {
                progressBar.fillAmount = 0;
                currentLevel++;
                BallSpawn.bs.IncreaseDifficulty();
                bc.BackgroundChange();
                if(currentLevel<10)
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

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Map");
    }

    public void Guardar(SaveType type, int data)
    {
        //Consider modifying this function so it can be called from everywhere in the game
        Debug.Log("Saving data: \n" + "highscore: " + data );
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);
        DatosAGuardar datos = new DatosAGuardar();
        switch (type)
        {
            case SaveType.HIGHSCORE:
                {
                    datos.puntuacionMaxima = data;
                    break;
                }
            
            default:
                break;
        }
       

        bf.Serialize(file, datos);

        file.Close();
    }

    void Cargar()
    {
        if (File.Exists(rutaArchivo))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(rutaArchivo, FileMode.Open);

            DatosAGuardar datos = (DatosAGuardar)bf.Deserialize(file);

            //Loading highscore;
             ScoreManager.sm.highScore = datos.puntuacionMaxima;
             ScoreManager.sm.UpdateHighScore(datos.puntuacionMaxima);
         
            file.Close();
        }
        else
        {
            Debug.Log("Not loading");
            
        }
    }


      
}

[Serializable]
class DatosAGuardar
{
    public int puntuacionMaxima;
    public int lifes; //Todo implement lifes;
}
