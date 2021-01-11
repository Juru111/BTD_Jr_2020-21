using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeramicBloon : DoubleBloon
{
    [SerializeField]
    private Sprite damagedCeramic1;
    [SerializeField]
    private Sprite damagedCeramic2;
    [SerializeField]
    private Sprite damagedCeramic3;
    [SerializeField]
    private Sprite damagedCeramic4;

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

        switch (healthPointsLeft)
        {
            case 8:
            case 7:
                mySpriteRenderer.sprite = damagedCeramic1;
                break;
            case 6:
            case 5:
                mySpriteRenderer.sprite = damagedCeramic2;
                break;
            case 4:
            case 3:
                mySpriteRenderer.sprite = damagedCeramic3;
                break;
            case 2:
            case 1:
                mySpriteRenderer.sprite = damagedCeramic4;
                break;
            default:
                break;
        }
    }
}
