using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public static BallSpawn bs;

    public GameObject[] ballsPrefab;

    public GameObject[] hexagonsPrefab;

    GameObject ball = null;

    public bool free;

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
        }
        
    }

    
    public void NewBall()
    {
        if(!FreezeManager.fm.freeze) 
        {
            free = false;
            ball = Instantiate(ballsPrefab[Random.Range(0, ballsPrefab.Length)], 
                new Vector2(AleatoryPosition(), transform.position.y), Quaternion.identity);

            BallManager.bm.balls.Add(ball);

            StartCoroutine(MoveDown());
        }
    }

    float AleatoryPosition()
    {
        return (Random.Range(-7.2f, 7.2f));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            free = false;
        }
    }

    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(1);

        while(!free)
        {
            ball.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - 0.5f);

            yield return new WaitForSeconds(1);
        }
    }
}
