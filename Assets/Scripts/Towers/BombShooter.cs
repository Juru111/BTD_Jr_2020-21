using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : Tower
{
    protected override void Path1toLv1()
    {
        AddRefreshRange(100);
    }
    protected override void Path1toLv2()
    {
        projectileType = ProjectileTypes.BombDarts;
    }
    protected override void Path1toLv3()
    {
        projectileType = ProjectileTypes.BombBombs;
    }
    protected override void Path1toLv4()
    {

    }

    protected override void Path2toLv1()
    {

    }
    protected override void Path2toLv2()
    {

    }
    protected override void Path2toLv3()
    {

    }
    protected override void Path2toLv4()
    {

    }
}
