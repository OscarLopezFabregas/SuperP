using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public static BallSpawn bs;

    public GameObject[] ballsPrefab;

    public GameObject[] hexagonsPrefab;

    GameObject ball = null;

    public bool free;

    float timeSpawn = 12f;

    private void Awake()
    {
        if(bs == null)
        {
            bs = this;
        }
        else if(bs!=this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(ball!=null && ball.transform.position.y <= 4.4f && !free)
        {
            free = true;
            ball.GetComponent<Ball>().StartForce(ball);
            BallManager.bm.balls.Add(ball);
            ball.gameObject.tag = "Ball";

            if(ball.GetComponent<Ball>().sprites.Length>0)
            {
                ball.name = ball.GetComponent<SpriteRenderer>().name;
            }
            ball = null;
        }
        
    }

    
    public void NewBall()
    {
        if(!FreezeManager.fm.freeze && ball == null) 
        {
            free = false;
            ball = Instantiate(ballsPrefab[Random.Range(0, ballsPrefab.Length)], 
                new Vector2(AleatoryPosition(), transform.position.y), Quaternion.identity);

     
            ball.gameObject.tag = "Untagged";
            StartCoroutine(MoveDown());
        }
    }

    float AleatoryPosition()
    {
        return (Random.Range(-7.2f, 7.2f));
    }

    public void IncreaseDifficulty()
    {
        if(timeSpawn>1)
        {
            timeSpawn -= 0.5f;
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            free = false;
        }
    }

    public IEnumerator MoveDown()
    {
        if(ball != null)
        {
            yield return new WaitForSeconds(1);

            while (!free)
            {
                if(FreezeManager.fm.freeze)
                {
                    break;
                }

                ball.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - 0.5f);

                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(timeSpawn);

            if(free)
            {
                NewBall();
            }
        }
      
    }
}
