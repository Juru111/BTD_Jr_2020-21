using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBloon : Bloon
{   
    [SerializeField]
    protected float spacing = 0.1f;

    public override void LayerPop(int power, GameObject parentPopProjectle)
    {
        //obliczenia właściwości dodatkowego balona
        BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
        Vector3 backDirection = transform.position - GameBox.instance.waypoints[myNextWaypoint].position;
        Vector3 anotherBloonPosition = transform.position + spacing * backDirection.normalized;
        //summonowanie dodatkowego bloona
        GameBox.instance.poolingMenager.SummonBloon(newBloonName, layersLeft - power, anotherBloonPosition, myNextWaypoint, distanceToWaypoint + spacing, isCammo, isRegrow, parentPopProjectle);
        
        base.LayerPop(power, parentPopProjectle);
    }
}