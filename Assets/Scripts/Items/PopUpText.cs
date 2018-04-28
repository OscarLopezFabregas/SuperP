using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{


    public Vector2 startPos;


    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * 2);

        if (transform.position.y > startPos.y + 2)
        {
            Destroy(gameObject);
        }


    }
}
