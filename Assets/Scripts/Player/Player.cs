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
        movement = Input.GetAxisRaw("Horizontal")*speed;
        animator.SetInteger("velX", Mathf.RoundToInt(movement));

        if(movement<0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
	}

    private void FixedUpdate()
    {
        if(leftWall)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                speed = 0f;
            }
            else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
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
            else if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKeyUp(KeyCode.RightArrow))
            {
                speed = 4f;
            }
        }


        rb.MovePosition(rb.position + Vector2.right*movement*Time.fixedDeltaTime);

        newX = Mathf.Clamp(transform.position.x, -8, 8);

        transform.position = new Vector2(newX, transform.position.y);
                
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
   
}
