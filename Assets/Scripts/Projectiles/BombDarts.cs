using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDarts : Bomb
{
    protected override void BombAction()
    {
        for (int i = 0; i < 8; i++)
        {
            GameBox.instance.poolingMenager.SummonProjectile(ProjectileTypes.Tack, transform.position,
                                                            1, 1, movementSpeed * 1.5f, i*45, 1f, canHitCamo);
        }
        
        base.BombAction();
    }
}
