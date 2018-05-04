﻿
using UnityEngine;

public class Fruits : MonoBehaviour {

    public GameObject fruitItem;
    	
    public void InstantiateFruit()
    {
        int fruitsInGame = GameObject.FindGameObjectsWithTag("Fruit").Length;

        if(fruitsInGame == 0 )
        {
            Instantiate(fruitItem, transform.position, Quaternion.identity);
        }
    }

    
}
