﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBox : MonoBehaviour
{
    public DataBase bloonsData;
    public PoolingMenager PoolingMenager;
    public static GameBox instance;
    #region Spis waypointów
    [field: SerializeField]
    public Transform[] waypoints { get; private set; }
    #endregion

    /*#region Zestaw prefabów
    [field: SerializeField]
    public GameObject[] bloonsPrefabs { get; private set; }
    #endregion*/

    private void Awake()
    {
        //chyba lepiej tu niż w każdym poolowanym obiekcie
        PoolingMenager = FindObjectOfType<PoolingMenager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
