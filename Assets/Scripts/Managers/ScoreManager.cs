using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager sm;

    public Text scoreText;
    public int currentScore = 0;

    public Text highScoreText;
    public int highScore = 500;


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

	void Update () {
		
	}
}
