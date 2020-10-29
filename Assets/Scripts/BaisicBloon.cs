using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaisicBloon : Bloon
{
    //private static DataBase dataBase;
    public override void Start()
    {
        base.Start();
        //layersLeft = 5;
    }

    public override void LayerPop(int power)
    {
        base.LayerPop(power);
        if(layersLeft == 4)
        {
            //Debug.Log(DataBase.Instancne.inscanceTest);
            //mySpriteRenderer.sprite = GameMenager.dataBaseInstancne.yellowBloonSprite;
            //mySpriteRenderer.sprite = dataBase.yellowBloonSprite;
            //movementSpeed = DataBase.yellowBloonSpeed;
        }
    }

    //void Update()
    //{
    //
    //}
}
