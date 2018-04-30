using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour {

    public static HexagonManager hm;

    public bool splitting;

    public List<GameObject> hexagons = new List<GameObject>();
 
    private void Awake()
    {
        if (hm == null)
        {
            hm = this;
        }
        else if (hm != this)
        {
            Destroy(gameObject);
        }

     
      
    }

    void Start()
    {
        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Hexagon"));

    }

 

    public void StartGame()
    {
        foreach (GameObject item in hexagons)
        {
            if (hexagons.IndexOf(item) % 2 == 0)
            {
                item.GetComponent<Hexagon>().right = true;
            }
            else
            {
                item.GetComponent<Hexagon>().right = false;
            }

            item.GetComponent<Hexagon>().StartForce(item);
        }
    }

    //Lose Game se tienen que llamar desde el GameManager

    public void LoseGame()
    {
        foreach (GameObject item in hexagons)
        {
            item.GetComponent<Hexagon>().forceX = 0f;
            item.GetComponent<Hexagon>().forceY = 0f;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void DestroyHexagon(GameObject hexagon, GameObject hex1, GameObject hex2)
    {
        hexagons.Remove(hexagon);
        Destroy(hexagon);
        hexagons.Add(hex1);
        hexagons.Add(hex2);
    }

    public void LastHexagon(GameObject hexagon)
    {
        Destroy(hexagon);
        hexagons.Remove(hexagon);
    }

    public void Dynamite(int maxNumberHexagons)
    {
        StartCoroutine(DynamiteH(maxNumberHexagons));
    }

    public void SlowTime()
    {
        StartCoroutine(TimeSlow());
    }

    //Liada...
    List<GameObject> FindHexagons(int typeOfHexagon)
    {
        List<GameObject> hexagonsToDestroy = new List<GameObject>();

        for (int i = 0; i < hexagons.Count; i++)
        {
            if (hexagons[i].GetComponent<Hexagon>().name.Contains(typeOfHexagon.ToString())
                && hexagons[i] != null)
            {
                hexagonsToDestroy.Add(hexagons[i]);
            }
        }
        return hexagonsToDestroy;
    }

    void ReloadList()
    {
        hexagons.Clear();

        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Hexagon"));

    }

    //Temporary bug fixing     //This is added to fix a bug realated to the GUN, easy to optimize!
    private void FixedUpdate()
    {
        int cnt = hexagons.Count;

        for (int i = 0; i<cnt; i++)
        {
            if(hexagons[i]== null)
            {
                ReloadList();
            }
        }
    }

    public IEnumerator DynamiteH(int maxNumberHexagons)
    {
        ReloadList();
              
        splitting = true;

        int numberToFind = 1;

        while (numberToFind < maxNumberHexagons)
        {
            foreach (GameObject item in FindHexagons(numberToFind))
            {
                item.GetComponent<Hexagon>().Split();
                    
                Destroy(item);
            }

            yield return new WaitForSeconds(0.2f);

            ReloadList();

            numberToFind++;
        }

        splitting = false;

    }

    public IEnumerator TimeSlow()
    {
        float time = 0;

        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().SlowHexagon();
            }
        }
        while (time < 3f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().NormalSpeedHexagon();
            }
        }
    }

  
}
