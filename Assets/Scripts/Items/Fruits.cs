
using UnityEngine;

public class Fruits : MonoBehaviour {

    public GameObject fruitItem;
    	
    public void InstantiateFruit()
    {
        Instantiate(fruitItem, transform.position, Quaternion.identity);
    }

    void Update()
    {

    }
}
