using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public Text ballsDestroyed;
    int balls;

    public Text totalFruits;
    int fruits;

    public Text totalTime;
    int time;

    public Text totalScoreCount;
    int totalScore;

    public Text gameScore;

    public string sceneToLoad;

    private void OnEnable()
    {
        balls = GameManager.gm.ballsDestroyed;
        ballsDestroyed.text = "X " + balls.ToString();

        fruits = GameManager.gm.fruitsCaught;
        totalFruits.text = "X " + fruits.ToString();

        time = (int) GameManager.gm.time;
        totalTime.text = time.ToString() + "s";

        SetTotalScore(ScoreManager.sm.currentScore);
        StartCoroutine(TotalScoreAmount());
    }

    void SetTotalScore (int score)
    {
        totalScore += score;

        totalScoreCount.text = totalScore.ToString();
    }

    public IEnumerator TotalScoreAmount()
    {
        yield return new WaitForSeconds(1);

        while(balls>0)
        {
            balls--;
            SetTotalScore(100);
            ballsDestroyed.text = "X " + balls.ToString();
            ScoreManager.sm.Updatescore(100);
            yield return new WaitForSeconds(0.1f);
        }

        while (fruits > 0)
        {
            fruits--;
            SetTotalScore(150);
            totalFruits.text = "X " + fruits.ToString();
            ScoreManager.sm.Updatescore(150);
            yield return new WaitForSeconds(0.1f);
        }

        while (time > 0)
        {
            time--;
            SetTotalScore(10);
            totalTime.text = time.ToString() + "s";
            ScoreManager.sm.Updatescore(10);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1);

        if(ScoreManager.sm.currentScore > ScoreManager.sm.highScore)
        {
            Debug.Log("Saving...");
            GameManager.gm.Guardar(SaveType.HIGHSCORE, ScoreManager.sm.highScore);
        }

        //Change to map
        if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            GameManager.gm.NextLevel(sceneToLoad);
        }

    }

  
}
