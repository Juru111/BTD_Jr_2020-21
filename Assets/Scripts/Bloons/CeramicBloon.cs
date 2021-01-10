using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeramicBloon : DoubleBloon
{
    private int healthPointsLeft = 10;
    public override void LayerPop(int power, GameObject _parentPopProjectle)
    {
        //w przypadku gdy SetMe() wywołą LayerPop() by obliczyć nadwyżkę obrażeń (obie metody z klasy Bloon)
        if (layersLeft < 9)
        {
            healthPointsLeft -= (9 - layersLeft);
            layersLeft = 9;
            if(healthPointsLeft < 1)
            {
                base.LayerPop(-healthPointsLeft + 1, _parentPopProjectle);
            }
        }
        //w pozostałych ("normalnych") przypadkach
        else
        {
            if (healthPointsLeft > power)
            {
                healthPointsLeft -= power;
                parentPopProjectle = _parentPopProjectle;
                GameBox.instance.poolingMenager.SummonPop(transform.position);
            }
            else
            {
                power -= healthPointsLeft;
                base.LayerPop(power + 1, _parentPopProjectle);
            }
        }
    }
}
