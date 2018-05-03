using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DontDestroy : MonoBehaviour {

    public static DontDestroy dd;

    private void Awake()
    {

        if (dd == null)
        {
            dd = this;
        }
        else if (dd != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

  
}
