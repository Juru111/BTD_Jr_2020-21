using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingOfFire : Projectile
{
    private float scaleFactor = 1;

    protected override void CalculateInitialProjectileData()
    {
        rangeLeft = range;
        scaleFactor = range / 1.224f; //liczba ta jest podstawowym zasięgiem (bez uleprzeń) tackShootera (DesignRange: 144)
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * scaleFactor, range / movementSpeed);
    }
}
