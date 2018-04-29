using System.Collections;
using UnityEngine;

public class Dinamite : MonoBehaviour {

    bool inGround;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!inGround)
        {
            transform.position += Vector3.down * Time.deltaTime * 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = true;
            StartCoroutine(WaitToBlink());
            Destroy(gameObject, 25f);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            BallManager.bm.Dynamite(5);
            HexagonManager.hm.Dynamite(5);
        }



    }

    IEnumerator WaitToBlink()
    {
        yield return new WaitForSeconds(20f);

        StartCoroutine(Blinking());
    }


    public IEnumerator Blinking()
    {
        while (sr != null)
        {
            sr.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
