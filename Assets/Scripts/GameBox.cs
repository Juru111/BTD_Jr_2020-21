using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBox : MonoBehaviour
{
    public DataBase bloonsData;
    public static GameBox instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
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
