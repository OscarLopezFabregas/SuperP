using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

    private Image button;

    private void Awake()
    {
        button = GetComponentInChildren<Image>();
    }

    // Use this for initialization
    void Start () {

        button.color = Social.localUser.authenticated ? Color.white : Color.grey;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if(Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkItby8-JQLEAIQAQ");
        }
        else
        {
            Social.localUser.Authenticate((bool success) => { });
        }
    }
}
