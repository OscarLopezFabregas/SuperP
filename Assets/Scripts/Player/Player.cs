using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speedX = 4f;
    public float speedY = 4f;

    public float movementX = 0f;
    public float horizontal = 0f;
    float movementY = 0f;

    
    //climb
    float maxClimbY = 0f;
    public bool climb;
    float defaultPosY;
    public bool inGround;
    public bool isUp;
    float fallingSpeed = 5f;
    float newX;
    float newY;
    bool newPosition;

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer sr;

    bool rightWall;
  
    bool leftWall;

    public GameObject shield;

    public bool blink;

    LifeManager lm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        lm = FindObjectOfType<LifeManager>();

    }

    void Start ()
    {
        rightWall = false;
        leftWall = false;

        lm.RestartLifesDoll();

        defaultPosY = transform.position.y;
    }
	
	
	void Update ()
    {
        if(GameManager.inGame)
        {
            // Debug.Log(movementX);
            movementX = horizontal * speedX;
            //movementX = Input.GetAxisRaw("Horizontal") * speedX;
            animator.SetInteger("velX", Mathf.RoundToInt(movementX));

            movementY = Input.GetAxisRaw("Vertical") * speedY;
            animator.SetInteger("velY", Mathf.RoundToInt(movementY));

            if (movementX < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
       
	}

    private void FixedUpdate()
    {
        if (GameManager.inGame)
        {
            //walls constrains
            if (leftWall)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || horizontal == -1)
                {
                    speedX = 0f;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)
                    ||horizontal != 0)
                {
                    speedX = 4f;
                }
            }
            if (rightWall )
            {

                if (Input.GetKey(KeyCode.RightArrow) || horizontal == 1)
                {
                    speedX = 0f;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
                   || horizontal != 0)
                {
                    speedX = 4f;
                }
            }
            
            
            //---

            //climbing
            if(transform.position.y >= maxClimbY)
            {
                isUp = true;
            }
            else
            {
                isUp = false;
            }

            if(climb)
            {
                if((Input.GetKey(KeyCode.UpArrow) && !isUp) 
                    || (Input.GetKey(KeyCode.DownArrow) && !inGround))
                {
                    speedY = 4f;
                }
                else
                {
                    speedY = 0;
                }
               
            }
            else
            {
                speedY = 0;
            }
            if(movementX!= 0)
            {
               // Debug.Log("Moving");
                rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
            }
           else if((transform.position.y >= defaultPosY && climb && !isUp)
                || (isUp && Input.GetKey(KeyCode.DownArrow)))
           {
                rb.MovePosition(rb.position + Vector2.up * movementY * Time.fixedDeltaTime);
            }

            //newX = Mathf.Clamp(transform.position.x, -8f, 8f);
            //transform.position = new Vector2(newX, transform.position.y);

            newY = Mathf.Clamp(transform.position.y, -2.31f, 8f);
            transform.position = new Vector2(transform.position.x, newY);

            if(!inGround && !climb)
            {
                transform.position += new Vector3(movementX / 5, -1) * Time.deltaTime * fallingSpeed;
            }
        }     
    }

    public void Win()
    {
        shield.SetActive(false);
        animator.SetBool("win", true);
    }

    private void OnBecameInvisible()
    {
        if(lm.lifes<=0)
        {
            GameManager.gm.GameOver();
        }
        else
        {
            Invoke("ReloadLevel", 0.5f);
        }
    }

    void ReloadLevel()
    {
        lm.SubtractLifes();
        lm.RestartLifesDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.inGame && !FreezeManager.fm.freeze)
        {
            if (collision.gameObject.tag == "Ball"
           || collision.gameObject.tag == "Hexagon")
            {
                if (shield.activeInHierarchy)
                {
                    shield.SetActive(false);

                    StartCoroutine(Blinking());
                }
                else
                {
                    if ((!blink))
                    {
                        //Sometimes Lose animation reproduce two times. 
                        StartCoroutine(Lose());
                    }
                }
            }
        }
        if (!GameManager.inGame && (collision.gameObject.tag == "Right"
            || collision.gameObject.tag == "Left"))
        {
            sr.flipX = !sr.flipX;
            rb.velocity /= 3f;
            rb.velocity *= -1f;
            rb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        }
        
       if(GameManager.inGame && collision.gameObject.tag == "Ladder")
        {
            if(!isUp)
            {
                maxClimbY = transform.position.y + collision.GetComponent<BoxCollider2D>().size.y - 0.1f;
            }
                   
        }
       if(GameManager.inGame && collision.gameObject.name.Contains("Platform")
            && transform.position.y + 0.3f < collision.gameObject.transform.position.y && inGround)
        {
            Debug.Log("trigger entered");
            transform.position = new Vector3(transform.position.x, collision.gameObject.transform.position.y);
            
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
        {
           
            leftWall = true;
        }
        else if (collision.gameObject.tag == "Right")
        {
            
            rightWall = true;
        }

        if(collision.gameObject.tag == "Ladder")
        {
            climb = true;
        }
      
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        //&& transform.position.y >= collision.gameObject.transform.position.y - 0.05f)
        {
            inGround = true;
            if (collision.gameObject.tag == "Platform"
                 && transform.position.y < collision.gameObject.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, collision.gameObject.transform.position.y);
              
            }
        }
      
    }

  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
        {
            leftWall = false;
        }
        else if (collision.gameObject.tag == "Right")
        {
            rightWall = false;
        }

        if(collision.gameObject.tag == "Ladder")
        {
            climb = false;
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            inGround = false;
        }
    }
   
    public IEnumerator Blinking()
    {
        blink = true;

        for(int i = 0; i<8; i++)
        {
            if(blink && GameManager.inGame)
            {
                sr.color = new Color(1,1,1,0);
                yield return new WaitForSeconds(0.2f);
                sr.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                break;
            }
        }

        blink = false;
    }

    public IEnumerator Lose()
    {
        //Consider testing more
        //Perhaps needs to reload BallList;

        GameManager.inGame = false;
      

        BallManager.bm.LoseGame();
        HexagonManager.hm.LoseGame();

        lm.LifeLose();

        animator.SetBool("loose", true);

        yield return new WaitForSeconds(1);

        rb.isKinematic = false;

        if(transform.position.x < 0)
        {
            rb.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);

        }

    }
}
