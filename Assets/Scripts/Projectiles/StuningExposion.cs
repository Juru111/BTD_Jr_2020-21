using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StuningExposion : Exposion
{
    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName != BloonTypes.MOAB)
        {
            bloonComponent.StunMe(0.6f);
        }
        base.ProjectileAction(bloonComponent);
    }

    protected override void CalculateInitialProjectileData()
    {
        canPopBlack = true;
        base.CalculateInitialProjectileData();
    }
}
