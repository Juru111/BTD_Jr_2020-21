using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Exposion : Projectile
{

    private float explosionRangeModifier = 1;
    protected override void Start()
    {
        base.Start();
        transform.DOScale(Vector3.one * explosionRangeModifier, 0.1f);
    }
    protected override void CalculateInitialProjectileData()
    {
        movementSpeed = 1;
        rangeLeft = 0.1f;
        if (popCountLeft > 40)
        {
            explosionRangeModifier = 1.5f;
        }
        transform.localScale = Vector3.zero;
    }

    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName != BloonTypes.Black && bloonComponent.bloonName != BloonTypes.Zebra)
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
