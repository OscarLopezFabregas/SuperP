using UnityEngine;

public class ShootManager : MonoBehaviour {

    public static ShootManager shm;

    public GameObject[] Shots;

    Transform player;

    public int maxShots;
    public int numberOfShots = 0;
    public int typeOfShot = 0; //0 - Arrow //1 - Double Arrow //2- Ancle //3- Laser

    Animator animator;

    CurrentShotImage shotImage;

    private void Awake()
    {
        if(shm == null)
        {
                shm = this;
        }
        else
        {
                Destroy(gameObject);
        }

        player = FindObjectOfType<Player>().transform;
        shotImage = FindObjectOfType<CurrentShotImage>();
        animator = player.GetComponent<Animator>();
    }
    
    private void Start()
    {
        if(GameManager.gm.gameMode == GameMode.TOUR)
        {
            typeOfShot = 0;
            maxShots = 1;
        }
        else
        {
            typeOfShot = 0;
            ChangeShot(1);
        }
    }

    private void Update()
    {
        if(CanShot() && (Input.GetKeyDown(KeyCode.X) || Buttons.shotButton))
        {
            Buttons.shotButton = false;
            Shot();
        }
        if( numberOfShots == maxShots && GameObject.FindGameObjectsWithTag("Arrow").Length == 0
            && GameObject.FindGameObjectsWithTag("Ancle").Length == 0)
        {
            numberOfShots = 0;
        }
        if(animator.GetBool("shoot") && player.GetComponent<Player>().movementX != 0)
        {
            animator.SetBool("shoot", false);
        }

    }
    bool CanShot()
    {
        if(numberOfShots < maxShots && GameManager.inGame)
        {
            return true;
        }

        return false;
    }
    void Shot()
    {
        if(player.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            animator.SetTrigger("shoot");
        }
        if (typeOfShot != 3)
        {
            Instantiate(Shots[typeOfShot], player.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Shots[3], new Vector2(player.position.x + .5f, player.position.y + 1),
                Quaternion.Euler(new Vector3(0, 0, -5)));
            Instantiate(Shots[3], new Vector2(player.position.x, player.position.y + 1),
               Quaternion.identity);
            Instantiate(Shots[3], new Vector2(player.position.x - .5f, player.position.y + 1),
               Quaternion.Euler(new Vector3(0, 0, 5)));
        }
        numberOfShots++;
    }

    public void DestroyShot()
    {
        if(numberOfShots>0 && numberOfShots<maxShots)
        {
            numberOfShots--;
        }
       
    }

    public void ChangeShot(int type)
    {
        //Debug.Log("changing");
        //Debug.Log("type:" + type);

        if (typeOfShot != type)
        {
            switch(type)
            {
                case 0:
                    maxShots = 1;
                    shotImage.CurrentShot("");
                    break;
                case 1:
                    maxShots = 2;
                    shotImage.CurrentShot("Arrow");
                    break;
                case 2:
                    maxShots = 1;
                    shotImage.CurrentShot("Ancle");
                    break;
                case 3:
                    maxShots = 15;
                    shotImage.CurrentShot("Gun");
                    break;

            }
        }
        typeOfShot = type;
        numberOfShots = 0;
    }
}
