using UnityEngine;

public class Hexagon : MonoBehaviour {

    public GameObject nextHexagon;

    Rigidbody2D rb;

    public float forceX = 4f;
    public float forceY = 4f;

    float currentForceX;
    float currentForceY;

    float rotSpeed;

    public bool right;

    public GameObject powerUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //When freeze I want them to keep rotating 
        if (GameManager.inGame)  //&& !FreezeManager.fm.freeze 
        {
            rotSpeed = 250 * Time.deltaTime;
            transform.Rotate(0, 0, rotSpeed);
            rb.velocity = new Vector2(forceX, forceY);
        }

        //ñapa
        if(GameManager.inGame && !FreezeManager.fm.freeze)
        {
            if(currentForceX == 0 && forceX == 0)
            {
                forceX = 4f;
                forceY = 4f;
            }
            
        }


    }

  //When instantiating Start() will be called, that could be a perfect moment to correct the;

        //bool in freeze not call freeze hexagon

    public void Split()
    {
        if (nextHexagon != null)
        {
            InstantiatePrice();

            GameObject hex1 = Instantiate(nextHexagon, rb.position + Vector2.right / 4,
                Quaternion.identity);
            hex1.GetComponent<Hexagon>().right = true;
            GameObject hex2 = Instantiate(nextHexagon, rb.position + Vector2.left / 4,
               Quaternion.identity);
            hex2.GetComponent<Hexagon>().right = false;
            if (FreezeManager.fm.freeze == false)
            {
                hex1.GetComponent<Hexagon>().forceX = forceX;
                hex1.GetComponent<Hexagon>().forceY = forceY;
                hex2.GetComponent<Hexagon>().forceX = -forceX;
                hex2.GetComponent<Hexagon>().forceY = forceY;

                HexagonManager.hm.DestroyHexagon(gameObject, hex1, hex2);
            }
            else
            {
                //BUG: currentForce se pone a 0 en el freeze. Ya que en esta version del script forceX/Y vale 0;
                           
                hex1.GetComponent<Hexagon>().currentForceX = 4f;
                hex1.GetComponent<Hexagon>().currentForceY = 4f;
               
                hex2.GetComponent<Hexagon>().currentForceX = -4f;
                hex2.GetComponent<Hexagon>().currentForceY = 4f;
              
                //Añadido:
                hex1.GetComponent<Hexagon>().forceX = 0;
                hex1.GetComponent<Hexagon>().forceY = 0;
                hex2.GetComponent<Hexagon>().forceX = 0;
                hex2.GetComponent<Hexagon>().forceY = 0;
            }

            if (!HexagonManager.hm.splitting)
            {
                HexagonManager.hm.DestroyHexagon(gameObject, hex1, hex2);
            }

        }
        else
        {
            HexagonManager.hm.LastHexagon(gameObject);
        }

    }

    public void StartForce(params GameObject[] hexagons)
    {
        for (int i = 0; i < hexagons.Length; i++)
        {
            if (right)
            {
                hexagons[i].GetComponent<Hexagon>().forceX = forceX;
            }
            else
            {
                hexagons[i].GetComponent<Hexagon>().forceX = -forceX;
            }
            hexagons[i].GetComponent<Hexagon>().forceY = forceY;

        }
    }

    public void FreezeHexagon(params GameObject[] hexagons)
    {
        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                currentForceX = item.GetComponent<Hexagon>().forceX;
                currentForceY = item.GetComponent<Hexagon>().forceY;

                item.GetComponent<Hexagon>().forceX = 0f;
                item.GetComponent<Hexagon>().forceY = 0f;


                item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

        }
    }

    public void UnfreezeHexagon(params GameObject[] hexagons)
    {
        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().forceX = currentForceX;
                item.GetComponent<Hexagon>().forceY = currentForceY;
            }

        }
    }

    public void SlowHexagon()
    {
        if(rb.velocity.x<0 && !FreezeManager.fm.freeze)
        {
            forceX = -2f;
        }
        else
        {
            forceX = 2f;
        }

        if (rb.velocity.y < 0)
        {
            forceY = -2f;
        }
        else
        {
            forceY = 2f;
        }
       
      
    }
    public void NormalSpeedHexagon()
    {
        if (rb.velocity.x < 0)
        {
            forceX = -4f;
        }
        else
        {
            forceX = 4f;
        }

        if (rb.velocity.y < 0)
        {
            forceY = -4f;
        }
        else
        {
            forceY = 4f;
        }

    }

    void InstantiatePrice()
    {
        int aleatory = GameManager.gm.AleatoryNumber();

        if (aleatory == 1)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground"|| collision.gameObject.tag == "Roof")
        {
            forceY *= -1;
        }
        if (collision.gameObject.tag == "Left" || collision.gameObject.tag == "Right")
        {
            forceX *= -1;
        }
    }
}
