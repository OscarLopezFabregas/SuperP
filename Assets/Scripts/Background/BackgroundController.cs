using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

    public Sprite[] backgrounds;

    Image currentBackground;



	void Start ()
    {
        currentBackground = GetComponent<Image>();

        if(GameManager.gm.gameMode == GameMode.PANIC)
        {
            BackgroundChange();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackgroundChange()
    {
        currentBackground.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
}
