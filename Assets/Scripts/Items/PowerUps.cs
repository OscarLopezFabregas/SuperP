using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    public Sprite[] powerUpsStatic;
    public GameObject[] powerUpsAnimated;
    bool inGround;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        int aleatory = Random.Range(0, 2);

        if(aleatory == 0)
        {
            sr.sprite = powerUpsStatic[Random.Range(0, powerUpsStatic.Length)];
            gameObject.name = sr.sprite.name;
        }
        else
        {
            Instantiate(powerUpsAnimated[Random.Range(0, powerUpsAnimated.Length)], transform.position,
            Quaternion.identity);
            Destroy(gameObject); //No creo que estoy vaya aqui...
        }
	}
	
	void Update ()
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

        if(collision.gameObject.tag == "Player")
        {
            if (gameObject.name.Equals("DoubleArrow"))
            {
                ShootManager.shm.ChangeShot(1);
            }
            else if (gameObject.name.Equals("Ancle"))
            {
                ShootManager.shm.ChangeShot(2);
            }
            else if (gameObject.name.Equals("Gun"))
            {
                ShootManager.shm.ChangeShot(3);
            }
            else if (gameObject.name.Equals("TimeStop"))
            {
               FreezeManager.fm.StartFreeze();
            }
            else if (gameObject.name.Equals("TimeSlow"))
            {
                BallManager.bm.SlowTime();
                HexagonManager.hm.SlowTime();
            }
            Destroy(gameObject);
        }

       

    }

    IEnumerator WaitToBlink()
    {
        yield return new WaitForSeconds(10f);

        StartCoroutine(Blinking());
    }


    public IEnumerator Blinking()
    {
        while(sr!=null)
        {
            sr.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }
       
    }
}
