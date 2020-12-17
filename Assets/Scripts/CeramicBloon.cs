using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeramicBloon : DoubleBloon
{
    private int healthPoints = 10;
    public override void LayerPop(int power, GameObject parentPopProjectle)
    {
        if (healthPoints > 1)
        {
            healthPoints -= 1;
        }
        else
        {
            Debug.Log("popnięto ceramic przy pozostałym życiu: " + healthPoints);
            base.LayerPop(power, parentPopProjectle);
        }
    }
}
