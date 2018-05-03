using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour {

    float time;
    Image borderColor;

    bool danger;

    Color normalColor;
    Color dangerColor;

    private void Awake()
    {
        borderColor = GetComponent<Image>();
    }

    // Use this for initialization
    void Start ()
    {
        normalColor = borderColor.color;
        dangerColor = new Color(240f/255, 0, 40f/255);
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = GameManager.gm.time;

        if(time<100 && !danger && GameManager.inGame)
        {
            StartCoroutine(Danger());
        }
     

	}

    IEnumerator Danger()
    {
        danger = true;

        while ( time> 0 && GameManager.inGame)
        {
            if(borderColor.color == normalColor)
            {
                borderColor.color = dangerColor;
            }
            else
            {
                borderColor.color = normalColor;
            }

            yield return new WaitForSeconds(0.4f);
        }

        borderColor.color = normalColor;
    }
}
