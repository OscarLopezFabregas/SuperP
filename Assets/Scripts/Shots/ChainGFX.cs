using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGFX : MonoBehaviour {

    Vector2 startPos;

	void Start ()
    {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = startPos;
	}
}
