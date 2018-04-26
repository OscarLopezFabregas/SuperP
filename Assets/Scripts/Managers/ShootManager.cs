using UnityEngine;

public class ShootManager : MonoBehaviour {

    public static ShootManager shm;

    public GameObject[] Shots;

    Transform player;

    public int maxShots;
    public int numberOfShots = 0;
    public int typeOfShot; //0 - Arrow //1 - Double Arrow //2- Ancle //3- Laser

    Animator animator;

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
    }
    
    private void Start()
    {
        typeOfShot = 0;
        maxShots = 1;
    }

    private void Update()
    {
        if(CanShot() && Input.GetKeyDown(KeyCode.X))
        {
            Shot();
        }

    }
    bool CanShot()
    {
        if(numberOfShots < maxShots)
        {
            return true;
        }

        return false;
    }
    void Shot()
    {   if(typeOfShot != 3)
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
        if(numberOfShots>0 && numberOfShots<=maxShots)
        {
            numberOfShots--;
        }
       
    }
}
