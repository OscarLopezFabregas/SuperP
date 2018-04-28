using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{

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
            Destroy(gameObject, 25f);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().shield.SetActive(true);
            collision.gameObject.GetComponent<Player>().blink = false;
            Destroy(gameObject);
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