using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject nextBall;

    Rigidbody2D rb;

    Vector2 currentVelocity;

    public bool right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    public void Split()
    {
        if(nextBall!=null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4,
                Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4,
               Quaternion.identity);
            if(FreezeManager.fm.freeze == false)
            {
                ball1.GetComponent<Rigidbody2D>().isKinematic = false;
                ball1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 5), ForceMode2D.Impulse);
                ball1.GetComponent<Ball>().right = true;
                ball2.GetComponent<Rigidbody2D>().isKinematic = false;
                ball2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 5), ForceMode2D.Impulse);
                ball2.GetComponent<Ball>().right = false;
                BallManager.bm.DestroyBall(gameObject, ball1, ball2);
            }
            else
            {
                ball1.GetComponent<Ball>().currentVelocity = new Vector2(2, 5);
                ball2.GetComponent<Ball>().currentVelocity = new Vector2(-2, 5);
            }

            if(!BallManager.bm.splitting)
            {
                BallManager.bm.DestroyBall(gameObject, ball1, ball2);
            }

        }
        else
        {
            BallManager.bm.LastBall(gameObject);
        }
        
    }

    public void StartForce(params GameObject[] balls)
    {
        for (int i = 0; i < balls.Length; i++)
        {

            balls[i].GetComponent<Rigidbody2D>().isKinematic = false;

            if (right)
            {
                balls[i].GetComponent<Rigidbody2D>().AddForce(Vector3.right * 2, ForceMode2D.Impulse);
            }
            else
            {
                balls[i].GetComponent<Rigidbody2D>().AddForce(Vector3.left * 2, ForceMode2D.Impulse);
            }
        }   
    }

    public void FreezeBall(params GameObject[] balls)
    {
        foreach(GameObject item in balls)
        {
            if(item!=null)
            {
             
                currentVelocity = item.GetComponent<Rigidbody2D>().velocity;
                item.GetComponent<Rigidbody2D>().isKinematic = true;
                item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            
        }
    }

    public void UnfreezeBall(params GameObject[] balls)
    {
         foreach(GameObject item in balls)
        {
            if(item!=null)
            {
                item.GetComponent<Rigidbody2D>().isKinematic = false;
                item.GetComponent<Rigidbody2D>().AddForce(currentVelocity, ForceMode2D.Impulse);
            }
        }
    }

    public void SlowBall()
    {
        rb.velocity /= 4f;
        rb.gravityScale = 0.25f;
     
    }
    public void NormalSpeedBall()
    {
        rb.velocity *= 4;
        rb.gravityScale = 1f;

    }

    //public IEnumerator WaitToBlink(params GameObject[] balls)
    //{
    //    yield return new WaitForSeconds(2f);
    //    foreach (GameObject item in balls)
    //    {
    //        StartCoroutine(Blinking(item));
    //    }

    //}

    //public IEnumerator Blinking(GameObject item)
    //{

    //    for(float i =0 ; i < 1f; i += Time.deltaTime)
    //    {
    //        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    //        yield return new WaitForSeconds(0.2f);
    //        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}  

}
