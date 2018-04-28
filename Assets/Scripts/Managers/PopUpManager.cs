using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {


    public GameObject popUpText;

    public static PopUpManager pop;

    private void Awake()
    {
        if (pop == null)
        {
            pop = this;
        }
        else if (pop != this)
        {
            Destroy(gameObject);
        }
    }

    public void InstantiatePopUpText(Vector2 startPos, int textScore)
    {
        GameObject pop = Instantiate(popUpText);

        pop.transform.position = startPos;

        pop.GetComponent<TextMesh>().text = textScore.ToString();
    }
}
