using UnityEngine;

public class Chain : MonoBehaviour {

    bool stopScaling = false;

    private void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "AncleTriggered");
    }

    void Update () {
        if(transform.localScale.y<7f && !stopScaling)
        {
            transform.localScale += Vector3.up * Time.deltaTime * 4f;
        }
	}

    void AncleTriggered(Notification notification)
    {
        stopScaling = true;
    }
    
}
