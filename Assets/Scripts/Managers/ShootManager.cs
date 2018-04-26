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
    {
        Instantiate(Shots[typeOfShot], player.position, Quaternion.identity);
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
