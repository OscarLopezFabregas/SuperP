using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezeManager : MonoBehaviour {

    public static FreezeManager fm;

    public Text freezeTimeText;
    public GameObject freezeTimeCount;
    public float freezeTime = 3f;
    public bool freeze = false;

    private void Awake()
    {
        if (fm == null)
        {
            fm = this;
        }
        else if (fm != this)
        {
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        freezeTimeCount.SetActive(false);
	}
	
	void Update ()
    {
		
	}

    public void StartFreeze(float freezeTime)
    {
        if(!freeze)
        {
           StartCoroutine(FreezeTime(freezeTime));
        }
    }

    public IEnumerator FreezeTime(float freezeTime)
    {
        BallManager.bm.ReloadList();


        freeze = true;

        foreach (GameObject item in BallManager.bm.balls)
        {
            if(item!=null)
            {
                item.GetComponent<Ball>().FreezeBall(item);
               // StartCoroutine(item.GetComponent<Ball>().WaitToBlink(item));
            }
        }

        foreach (GameObject item in HexagonManager.hm.hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().FreezeHexagon(item);
                // StartCoroutine(item.GetComponent<Ball>().WaitToBlink(item));
            }
        }

        

        freezeTimeCount.SetActive(true);

        while(freezeTime>0)
        {
            
            freezeTimeText.text = freezeTime.ToString("f2");
            freezeTime -= Time.deltaTime;
            yield return null;
        }
            
        freezeTimeCount.SetActive(false);
        freezeTime = 0f;

       
        foreach (GameObject item in BallManager.bm.balls)
        {
            if (item != null)
            {
                item.GetComponent<Ball>().UnfreezeBall(item);
            }
          
        }

        foreach (GameObject item in HexagonManager.hm.hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().UnfreezeHexagon(item);
                // StartCoroutine(item.GetComponent<Ball>().WaitToBlink(item));
            }
        }

       
        freeze = false;
    }

 


 
}

