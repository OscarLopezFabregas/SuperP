using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PressStart : MonoBehaviour {

    public Text pressStart;
    float time;
    Color transparent;
    private void Start()
    {
        
        GameObject dontDestroy = FindObjectOfType<DontDestroy>().gameObject;

        if(dontDestroy!= null)
        {
            Destroy(dontDestroy);
        }
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

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
