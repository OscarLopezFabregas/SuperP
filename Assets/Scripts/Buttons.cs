using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Player player;

    public static bool shotButton;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Equals("Right")) 
        {
            player.horizontal = 1;
        }
        else if (gameObject.name.Equals("Left"))
        {
            player.horizontal = -1;
        }
        else if (gameObject.name.Equals("Shot"))
        {
            shotButton = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(gameObject.name.Equals("Right")|| gameObject.name.Equals("Left"))
        {
            player.horizontal = 0;

        }
        else if (gameObject.name.Equals("Shot"))
        {
            shotButton = false;
        }

    }
}
