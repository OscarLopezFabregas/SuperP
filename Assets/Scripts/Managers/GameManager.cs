using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public static bool inGame;

    public GameObject ready;

    Player player;

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
