using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOAB : Bloon
{
    [SerializeField]
    protected float spacing = 0.1f;
    private int healthPoints = 200;
    public override void LayerPop(int power, GameObject _parentPopProjectle)
    {
        if (healthPoints > 1)
        {
            healthPoints -= 1;
            parentPopProjectle = _parentPopProjectle;
            GameBox.instance.PoolingMenager.SummonPop(transform.position);
        }
        else
        {
            //obliczenia właściwości dodatkowych balonów
            BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
            Vector3 backDirection = transform.position - GameBox.instance.waypoints[myNextWaypoint].position;
            //summonowanie dodatkowego bloonów
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position +     spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint +     spacing, isCammo, isRegrow, _parentPopProjectle);
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position + 2 * spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint + 2 * spacing, isCammo, isRegrow, _parentPopProjectle);
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft - 1, transform.position + 3 * spacing * backDirection.normalized, myNextWaypoint, distanceToWaypoint + 3 * spacing, isCammo, isRegrow, _parentPopProjectle);

            base.LayerPop(power, _parentPopProjectle);
        }
    }

    //protected override void ChangeWaypoint()
    //{
    //    base.ChangeWaypoint();
    //    Vector3 moveDirection = GameBox.instance.waypoints[myNextWaypoint].position - transform.position;
    //    float spriteRotationAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(spriteRotationAngle - 90, Vector3.forward);
    //}


}
