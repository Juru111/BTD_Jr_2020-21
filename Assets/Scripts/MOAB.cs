using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOAB : Bloon
{
    [SerializeField]
    protected float spacing = 0.1f;
    private int healthPoints = 200;
    public override void LayerPop(int power, GameObject parentPopProjectle)
    {
        if (healthPoints > 1)
        {
            healthPoints -= 1;
        }
        else
        {
            //obliczenia właściwości dodatkowych balonów
            BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
            Vector3 backDirection = transform.position - GameBox.instance.waypoints[myNextWaypoint].position;
            //summonowanie dodatkowego bloonów
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position +     spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint +     spacing, isCammo, isRegrow, parentPopProjectle);
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position + 2 * spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint + 2 * spacing, isCammo, isRegrow, parentPopProjectle);
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position + 3 * spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint + 3 * spacing, isCammo, isRegrow, parentPopProjectle);

            base.LayerPop(power, parentPopProjectle);
        }
    }
}
