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
        //popCountLeft nie jest używany według nazwy, jest dla przekazania informacji o zwiękrzonym zasięgu bomby 
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
        transform.localScale = Vector3.zero;//new Vector3(0.9f, 0.9f, 0.9f);
    }

    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName != BloonTypes.Black && bloonComponent.bloonName != BloonTypes.Zebra)
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
