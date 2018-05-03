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


public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public static bool inGame;

    public GameObject ready;

    Player player;

    public float time = 100f;

    public Text timeText;

    public GameObject panel;
    PanelController panelController;

    LifeManager lm;
    Fruits fruits;

    public int ballsDestroyed = 0;
    public int fruitsCaught;

    string rutaArchivo;
       
    private void Awake()
    {
        rutaArchivo = Application.persistentDataPath + "/datos.dat";

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
    }
    
	
	void Start ()
    {
        Cargar();
        StartCoroutine(GameStart());
	}
	
	
	void Update ()
    {
	     if(BallManager.bm.balls.Count == 0 
          && HexagonManager.hm.hexagons.Count == 0)
        {
            inGame = false;

            player.Win();
            lm.LifeWin();
            panel.SetActive(true);
            panelController = panel.GetComponent<PanelController>();
        }

         if(inGame)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME: " + time.ToString("f0");
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

        BallManager.bm.StartGame();
        HexagonManager.hm.StartGame();

        GameManager.inGame = true;
    }

    public int AleatoryNumber()
    {
        return UnityEngine.Random.Range(0, 3);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Guardar(SaveType type, int data)
    {
        //Consider modifying this function so it can be called from everywhere in the game
       // Debug.Log("Saving data: \n" + "highscore: " + data );
        
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
