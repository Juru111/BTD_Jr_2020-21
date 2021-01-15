using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkey : Tower
{
    [SerializeField]
    private Sprite spikeOPult;
    [SerializeField]
    private Sprite tripleDart;
    [SerializeField]
    private bool isTripleProjectile;

    protected override void Path1toLv1() 
    {
        AddRefreshRange(50);
    }
    protected override void Path1toLv2()
    {
        AddRefreshRange(50);
        canSeeCamo = true;
    }
    protected override void Path1toLv3()
    {
        mySpriteRenderer.sprite = spikeOPult;
        projectilePierce += 16;
        projectileType = ProjectileTypes.SpikedBall;
        attackSpeed = 1.15f;
        projectileSpeed *= 0.5f;
        projectileRange = range * 5;
    }
    protected override void Path1toLv4()
    {
        AddRefreshRange(50);
        projectileRange = range * 5;
        projectilePierce += 82;
        projectileType = ProjectileTypes.BigSpikedBall;
    }
    protected override void Path2toLv1()
    {
        projectilePierce += 1;
    }
    protected override void Path2toLv2() 
    {
        projectilePierce += 2;
    }
    protected override void Path2toLv3()
    {
        mySpriteRenderer.sprite = tripleDart;
        isTripleProjectile = true;
    }
    protected override void Path2toLv4() 
    {

    }


    protected override IEnumerator TowerAttack()
    {
        if (isTripleProjectile)
        {
            if (target != null)
            {
                //kalkulacje oraz summon projectile-ów
                for (int i = -1; i < 2; i++)
                {
                    float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180 / Mathf.PI;
                    GameBox.instance.poolingMenager.SummonProjectile(projectileType, transform.position, projectilePierce, projectilePower, projectileSpeed, angle+15*i, projectileRange, canSeeCamo);
                }
            }
                
            else
            {
                Debug.LogError("target is emppty");
            }
            yield return new WaitForSeconds(attackSpeed);
            isAttackCorutine = false;
        }
        else
        {
            StartCoroutine(base.TowerAttack());
            yield return null;
        }
    }
}
