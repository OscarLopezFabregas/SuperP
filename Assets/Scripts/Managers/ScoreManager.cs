using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager sm;

    public Text scoreText;
    public int currentScore = 0;

    public Text highScoreText;
    private int _highScore = 0;
    private int key = 0;

    public int highScore
    {
        get { return _highScore ^ key; }
        set
        {
            key = Random.Range(0, int.MaxValue);
            _highScore = value ^ key;
        }
    }

    private void Awake()
    {
        if (sm == null)
        {
            sm = this;
        }
        else if (sm != this)
        {
            Destroy(gameObject);
        }

     }

  
    void Start ()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
	}
	
	public void Updatescore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();

        if(currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "HI - " + highScore.ToString();
         }
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("On app quit do:");
        //Debug.Log(currentScore);
        //Debug.Log(highScore);
        if(currentScore>= highScore)
        {
           // Debug.Log("current score > highscore");
            GameManager.gm.Guardar(SaveType.HIGHSCORE, highScore);
        }
    }

    public void UpdateHighScore(int data)
    {
        highScoreText.text ="HI - " + data.ToString();
    }
}
