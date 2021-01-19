using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBombs : Bomb
{
    protected override void BombAction()
    {
        for (int i = 0; i < 8; i++)
        {
            GameBox.instance.poolingMenager.SummonProjectile(ProjectileTypes.Bomb, transform.position,
                                                            10, 1, movementSpeed * 1.5f, i * 45, range, canHitCamo);
        }

        base.BombAction();
    }
}
