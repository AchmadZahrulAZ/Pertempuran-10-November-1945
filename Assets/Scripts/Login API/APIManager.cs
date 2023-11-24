using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class APIManager : MonoBehaviour 
{
    private static APIManager instance;
    public static APIManager Instance { get { return instance; } }
    public static string baseURL = "https://mtsnuhati.com/sigamingclub/api/inc/";
    public Account account { get; private set; }
    public string previousAccessToken { get; private set;} //Subject to change, whether the API should be able to read the previous access token or not
    const string FILENAME = "meta.dat";
    public const int ID_GAME = 3; //Pertempuran 10 November 1945's ID in the database is 1


    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

        // ReadData();
    }

    public void SetAccount(Account account)
    {
        this.account = account;
        // if(account != null)
        // {
        //     WriteData(account.accessToken);
        // }
        // else
        // {
        //     WriteData("");
        // }
    }
}