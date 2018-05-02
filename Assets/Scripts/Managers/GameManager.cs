using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public static bool inGame;

    public GameObject ready;

    Player player;

    float time = 100f;

    public Text timeText;

    public GameObject panel;
    PanelController panelController;

    LifeManager lm;
    Fruits fruits;

    public int ballsDestroyed = 0;
    public int fruitsCaught;

    private void Awake()
    {
        if(gm==null)
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

        if(ballsDestroyed%Random.Range(5,20)==0 && BallManager.bm.balls.Count >0)
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
        return Random.Range(0, 3);
    }
}
