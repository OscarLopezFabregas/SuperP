using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : MonoBehaviour {
    Animator animator;
    public GameObject powerUp;
	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
     
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroying");
        if (collision.gameObject.tag == "Ancle" ||
             collision.gameObject.tag == "Arrow" ||
             collision.gameObject.tag == "Laser")
        {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        animator.SetTrigger("destroy");
        yield return new WaitForSeconds(0.15f);
        Instantiate(powerUp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
