using UnityEngine;

public class ShotArrow : MonoBehaviour {

    float speed = 4f;
    public GameObject chainGFX;
    Vector2 startPos;


    private void Start()
    {
        startPos = transform.position;

        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);

        chain.transform.parent = transform;

        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.fixedDeltaTime;

        if((transform.position.y - startPos.y)>=0.2f)
        {
           GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);

           chain.transform.parent = transform;

            startPos = transform.position; 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag !="Player")
        {
            Destroy(gameObject);
            ShootManager.shm.DestroyShot();
        }
             
    }
}
