using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public enum SaveType
{
    HIGHSCORE,
    LIFE,
};

public class GameStatus : MonoBehaviour {

    public static GameStatus gs;

    private string rutaArchivo;

    public int gameHighScore;

    private void Awake()
    {
       

        if (gs == null)
        {
            gs = this;
            DontDestroyOnLoad(gameObject);
           // PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
        else if (gs != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {

        //check directoryseparatorchar
        rutaArchivo = Application.persistentDataPath +  "/datos.dat"; //Path.DirectorySeparatorChar +
        Debug.Log(rutaArchivo);
        Debug.Log(File.Exists(rutaArchivo));
        
        Cargar();

        ((PlayGamesPlatform)Social.Active).Authenticate((bool success) => { }, true);

        //string[] testDeviceIDs = new string[] { "E92E9A6745B85439C2EA180AB0010A87" };
       // EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);

        //Add for testing
        //EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("ca-app-pub-3940256099942544/1033173712");

        //Comercial Add
        //EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("ca-app-pub-4618159390638701/1273555085");

    }

    // Update is called once per frame
    void Update () {
		
	}

      public void GuardarHighScore(int gameHighScore) 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);
        DatosAGuardar datos = new DatosAGuardar();


        datos.puntuacionMaxima = gameHighScore;
        Debug.Log(gameHighScore);
        bf.Serialize(file, datos);

        file.Close();
    }


    public void Guardar(SaveType type, int data)
    {
        //Consider modifying this function so it can be called from everywhere in the game
        Debug.Log("Saving data: \n" + "highscore: " + data);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);
        DatosAGuardar datos = new DatosAGuardar();
        switch (type)
        {
            case SaveType.HIGHSCORE:
                {
                    datos.puntuacionMaxima = data;
                    break;
                }

            default:
                break;
        }


        bf.Serialize(file, datos);

        file.Close();
    }
    void Cargar()
    {
        if (File.Exists(rutaArchivo))
        {
            Debug.Log("Loading...");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(rutaArchivo, FileMode.Open);

            DatosAGuardar datos = (DatosAGuardar)bf.Deserialize(file);
            
            //Loading highscore;
            gameHighScore = datos.puntuacionMaxima;

            Debug.Log(gameHighScore);
           // ScoreManager.sm.UpdateHighScore(datos.puntuacionMaxima);

            file.Close();
        }
        else
        {
            Debug.Log("Not loading");
            gameHighScore = 666;

        }
    }

    private void OnApplicationQuit()
    {
        GuardarHighScore(gameHighScore);
    }

}



[Serializable]
class DatosAGuardar
{
    public int puntuacionMaxima;
    public int lifes; //Todo implement lifes;
}

