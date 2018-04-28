using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public static BallManager bm;

    public bool splitting;

    
    private void Awake()
    {
        if (bm == null)
        {
            bm = this;
        }
        else if (bm != this)
        {
            Destroy(gameObject);
        }
    }


    public List<GameObject> balls = new List<GameObject>();
	

	void Start ()
    {
        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        StartGame();
        
	}
	
	
	void Update () {
		
	}

    public void StartGame()
    {
        foreach(GameObject item in balls)
        {
            if(balls.IndexOf(item)%2 == 0)
            {
                item.GetComponent<Ball>().right = true;
            }
            else
            {
                item.GetComponent<Ball>().right = false;
            }

            item.GetComponent<Ball>().StartForce(item);
        }
    }

    public void DestroyBall(GameObject ball, GameObject ball1, GameObject ball2)
    {
        Destroy(ball);
        balls.Remove(ball);
        balls.Add(ball1);
        balls.Add(ball2);
    }

    public void LastBall(GameObject ball)
    {
        Destroy(ball);
        balls.Remove(ball);
    }

    public void Dynamite(int maxNumberBalls)
    {
        StartCoroutine(DynamiteBH(maxNumberBalls));
    }

    //Liada...
    List<GameObject> FindBalls(int typeOfBall)
    {
        List<GameObject> ballsToDestroy = new List<GameObject>();

        for(int i=0; i<balls.Count; i++)
        {
            if (balls[i].GetComponent<Ball>().name.Contains(typeOfBall.ToString())
                && balls[i] != null)
            {
                ballsToDestroy.Add(balls[i]);
            }
        }
        return ballsToDestroy;
    }

    void ReloadList()
    {
        balls.Clear();

        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
                
    }

    public IEnumerator DynamiteBH(int maxNumberBalls)
    {
        ReloadList();

        splitting = true;

        int numberToFind = 1;

        while(numberToFind<maxNumberBalls)
        {
            foreach (GameObject item in FindBalls(numberToFind))
            {
                item.GetComponent<Ball>().Split();

                Destroy(item);
            }

            yield return new WaitForSeconds(0.2f);

            ReloadList();

            numberToFind++;
        }

        splitting = false;
       
    }
}
