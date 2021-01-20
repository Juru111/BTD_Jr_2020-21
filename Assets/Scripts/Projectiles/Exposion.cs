using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Exposion : Projectile
{

    private float explosionRangeModifier = 1;
    protected bool canPopBlack = false;

    protected override void CalculateInitialProjectileData()
    {
        movementSpeed = 1;
        rangeLeft = 0.1f;
        if (popCountLeft > 40)
        {
            explosionRangeModifier = 1.5f;
        }
        else if (popCountLeft == 40)
        {
            explosionRangeModifier = 1;
        }
        else if (popCountLeft < 40)
        {
            explosionRangeModifier = 0.6f;
        }
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * explosionRangeModifier, 0.1f);
    }

    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (canPopBlack || (bloonComponent.bloonName != BloonTypes.Black && bloonComponent.bloonName != BloonTypes.Zebra))
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
