using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MOABExposion : Exposion
{
    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName == BloonTypes.MOAB)
        {
            popCountLeft--;
            bloonComponent.LayerPop(power * 10, gameObject);
            if (popCountLeft <= 0)
            {
                GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
            }
        }
        else
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
