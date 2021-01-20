using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : Tower
{
    [SerializeField]
    private Sprite missileLauncher;
    [SerializeField]
    private Sprite MOABMauler;
    [SerializeField]
    private Sprite clusterBombs;

    protected override void Path1toLv1()
    {
        AddRefreshRange(100);
    }
    protected override void Path1toLv2()
    {
        if (path2Lv < 2) { projectileType = ProjectileTypes.BombDarts; }
        else { projectileType = ProjectileTypes.MOABMaulerDarts; }
    }
    protected override void Path1toLv3()
    {
        if (path2Lv < 2) { projectileType = ProjectileTypes.BombBombs; }
        else { projectileType = ProjectileTypes.MissileBombs; }
        mySpriteRenderer.sprite = clusterBombs;
    }
    protected override void Path1toLv4()
    {
        if (path2Lv < 2) { projectileType = ProjectileTypes.StuningBombBombs; }
        else { projectileType = ProjectileTypes.StuningMissileBombs; }
    }

    protected override void Path2toLv1()
    {
        projectilePierce += 10;
    }
    protected override void Path2toLv2()
    {
        projectilePierce += 5;
        attackSpeed = 1.33f;
        AddRefreshRange(60);
        projectileSpeed *= 1.2f;
        switch (path1Lv)
        {
            case 0:
            case 1: projectileType = ProjectileTypes.Missile;
                    mySpriteRenderer.sprite = missileLauncher; break;
            case 2: projectileType = ProjectileTypes.MissileDarts;
                    mySpriteRenderer.sprite = missileLauncher; break;
            case 3: projectileType = ProjectileTypes.MissileBombs; break;
            case 4: projectileType = ProjectileTypes.StuningMissileBombs; break;
            default: break;
        }
    }
    protected override void Path2toLv3()
    {
        attackSpeed = 1.2f;
        if (path1Lv < 2) { projectileType = ProjectileTypes.MOABMauler; }
        else { projectileType = ProjectileTypes.MOABMaulerDarts; }
        mySpriteRenderer.sprite = MOABMauler;
    }
    protected override void Path2toLv4()
    {

    }

}
