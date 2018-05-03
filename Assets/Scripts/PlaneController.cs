using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

    public Transform[] nodes;
    float step = 1f;

    //with notification center we will know when a world has been completed;
    private void Awake()
    {
        NotificationCenter.DefaultCenter().AddObserver(this,"MoveToNextNode");
    }


    public void MoveToNextNode(Notification notification)
    {
        int i = (int)notification.data;
        transform.position = Vector3.MoveTowards(transform.position, nodes[i].position, step * Time.deltaTime);
        transform.LookAt(nodes[i]);
    }
}
