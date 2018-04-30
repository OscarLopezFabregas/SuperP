using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 4f;

    float movement = 0f;

    float newX;

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer sr;

    bool rightWall;
  
    bool leftWall;

    public GameObject shield;

    public bool blink;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

       
    }

    void Start ()
    {
        rightWall = false;
        leftWall = false;
    }
	
	
	void Update ()
    {
        if(GameManager.inGame)
        {
            movement = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetInteger("velX", Mathf.RoundToInt(movement));

            if (movement < 0)
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
            if (leftWall)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    speed = 0f;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    speed = 4f;
                }
            }
            if (rightWall)
            {

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    speed = 0f;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    speed = 4f;
                }
            }


            rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);

            newX = Mathf.Clamp(transform.position.x, -8, 8);

            transform.position = new Vector2(newX, transform.position.y);
        }     
    }

    public void Win()
    {
        shield.SetActive(false);
        animator.SetBool("win", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.inGame && !FreezeManager.fm.freeze)
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

            if (!GameManager.inGame && (collision.gameObject.tag == "Right"
                || collision.gameObject.tag == "Left"))
            {
                sr.flipX = !sr.flipX;
                rb.velocity /= 3f;
                rb.velocity *= -1f;
                rb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
            }
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
