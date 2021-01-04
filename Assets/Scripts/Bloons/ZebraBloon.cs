using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraBloon : Bloon
{
    [SerializeField]
    protected float spacing = 0.1f;

    public override void LayerPop(int power, GameObject parentPopProjectle)
    {
        //obliczenia właściwości białego balona
        Vector3 backDirection = transform.position - GameBox.instance.waypoints[myNextWaypoint].position;
        Vector3 anotherBloonPosition = transform.position + spacing * backDirection.normalized;
        //summonowanie białego bloona
        GameBox.instance.poolingMenager.SummonBloon(BloonTypes.White, layersLeft - 1, anotherBloonPosition, myNextWaypoint, distanceToWaypoint + spacing, isCammo, isRegrow, parentPopProjectle);

        base.LayerPop(power, parentPopProjectle);
    }
}
