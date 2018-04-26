using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectMode : MonoBehaviour {

    public Image tourModeImage;
    public Text tourModeText;
    public Image panicModeImage;
    public Text panicModeText;

    bool tour;
    Color yellowAlpha;
    
	// Use this for initialization
	void Start ()
    {
        tour = true;
        yellowAlpha = new Color(1f, 1f, 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (tour)
        {
            tourModeImage.color = Color.white;
            tourModeText.color = Color.white;

            panicModeImage.color = yellowAlpha;
            panicModeText.color = yellowAlpha;
        }
        else
        {
            tourModeImage.color = yellowAlpha;
            tourModeText.color = yellowAlpha;

            panicModeImage.color = Color.white;
            panicModeText.color = Color.white;
        }
    }
    public void TourModeClicked()
    {
        tour = true;
    }
    public void PanicModeClicked()
    {
        tour = false;
    }

    public void LoadSceneMode()
    {
        if(tour)
        {
            SceneManager.LoadScene("Tour_01");
        }
        else
        {
            SceneManager.LoadScene("Panic");
        }
    }
}
