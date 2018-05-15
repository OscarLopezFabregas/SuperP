using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        button.interactable = Social.localUser.authenticated ? true : false;
    }

    public void ShowLeaderScore()
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
