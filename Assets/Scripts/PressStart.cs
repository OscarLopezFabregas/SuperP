using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PressStart : MonoBehaviour {

    public Text pressStart;
    float time;
    Color transparent;
    private void Start()
    {
        transparent = new Color(1, 1, 1, 0);
    }

    void Update ()
    {
        time += Time.deltaTime*3;

        if(Mathf.RoundToInt(time)%2 == 0)
        {
            pressStart.color = Color.yellow;
        }
        else
        {
            pressStart.color = transparent;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("selectMode");
    }
}
