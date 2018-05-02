using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour {

    bool inGround;

    SpriteRenderer sr;

    public Sprite[] fruitsSprites;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.sprite = fruitsSprites[Random.Range(0, fruitsSprites.Length)];
        gameObject.name = sr.sprite.name;
    }

    private void Update()
    {
        if(!inGround)
        {
            transform.position += Vector3.down * Time.deltaTime * 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            inGround = true;
            StartCoroutine(WaitToBlink());
            Destroy(gameObject, 15f);
        }
        else if(collision.gameObject.tag == "Player" 
            || collision.gameObject.tag == "Ancle" 
            || collision.gameObject.tag == "Laser"
            || collision.gameObject.tag == "Arrow")
        {
            int score = Random.Range(500, 1000);
            PopUpManager.pop.InstantiatePopUpText(transform.position, score);
            Destroy(gameObject);
            ScoreManager.sm.Updatescore(score);
            GameManager.gm.fruitsCaught++;
        }
    }


    IEnumerator WaitToBlink()
    {
        yield return new WaitForSeconds(10f);

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
