using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBox : MonoBehaviour
{
    public DataBase dataBase;
    public PoolingMenager poolingMenager;
    public UIMenager uIMenager;
    public static GameBox instance;

    //spis waypointów
    [field: SerializeField]
    public Transform[] waypoints { get; private set; }

    

    private void Awake()
    {
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

    //potrzebne dla roboczego restartu gry z MessageWindow
    public void SetInstanceToNull()
    {
        instance = null;
    }
}
