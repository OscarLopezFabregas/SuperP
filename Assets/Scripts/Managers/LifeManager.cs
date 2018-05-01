using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public int lifes = 3;

    public Text lifesText;

    public GameObject lifeDoll;

    Animator animator;

    private void Awake()
    {
        animator = lifeDoll.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	public void UpdateLifes (int life) {

        if(life > 0)
        {
            lifes += life;
        }
        else
        {
            lifes -= life;
        }

        lifesText.text = "X " + lifes.ToString();

	}

    public void LifeWin()
    {
        animator.SetBool("win", true);
    }

    public void LifeLose()
    {
        animator.SetBool("lose", true);
    }

}
