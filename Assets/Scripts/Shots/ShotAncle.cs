using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAncle : MonoBehaviour {

    float speed = 4f;
    public GameObject chainGFX;
    Vector2 startPos;
    List<GameObject> chains = new List<GameObject>();

    private void Start()
    {
        startPos = transform.position;

        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);

        chain.transform.parent = transform;

        chains.Add(chain);

        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.fixedDeltaTime;

        if ((transform.position.y - startPos.y) >= 0.2f)
        {
            GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);

            chain.transform.parent = transform;

            chains.Add(chain);
            
            startPos = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NotificationCenter.DefaultCenter().PostNotification(this,"AncleTriggered");

        if(collision.gameObject.tag == "Roof")
        {
            StartCoroutine(DestroyAncle());
        }

        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Ball>().Split();
            Destroy(gameObject);
            ShootManager.shm.DestroyShot();
        }
        if (collision.gameObject.tag == "Hexagon")
        {
            collision.gameObject.GetComponent<Hexagon>().Split();
            Destroy(gameObject);
            ShootManager.shm.DestroyShot();
        }

    }

    IEnumerator DestroyAncle()
    {
        speed = 0f;

        yield return new WaitForSeconds(1f);

        GetComponentInParent<SpriteRenderer>().color = Color.yellow;

        foreach (GameObject item in chains)
        {
            item.GetComponent<SpriteRenderer>().color = Color.yellow; 
        }

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        ShootManager.shm.DestroyShot();
    }

}
