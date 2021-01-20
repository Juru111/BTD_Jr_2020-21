using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBombs : Bomb
{
    protected override void BombAction()
    {
        for (int i = 0; i < 8; i++)
        {
            GameBox.instance.poolingMenager.SummonProjectile(ProjectileTypes.BombFullRange, transform.position,
                                                            popCountLeft/8, 1, movementSpeed * 0.4f, i * 45, range/3, canHitCamo);
        }

        base.BombAction();
    }
}
