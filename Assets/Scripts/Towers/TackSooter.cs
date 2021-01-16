using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackSooter : Tower
{
    [SerializeField]
    protected int projectileCount = 8;
    [SerializeField]
    private Sprite tackSprayer;
    [SerializeField]
    private Sprite ringOfFire;
    [SerializeField]
    private Sprite bladeShooter;

    protected override void Path1toLv1()
    {
        attackSpeed = 1.25f;
    }
    protected override void Path1toLv2()
    {
        attackSpeed = 0.69f;
    }
    protected override void Path1toLv3()
    {
        mySpriteRenderer.sprite = tackSprayer;
        projectileCount += 8;
    }
    protected override void Path1toLv4()
    {
        mySpriteRenderer.sprite = ringOfFire;
        projectileSpeed *= 0.5f;
        projectileCount = 1;
        projectileType = ProjectileTypes.RingOfFire;
        projectilePierce += 59;
    }

    protected override void Path2toLv1()
    {
        AddRefreshRange(21);
    }
    protected override void Path2toLv2()
    {
        AddRefreshRange(25);
    }
    protected override void Path2toLv3()
    {
        mySpriteRenderer.sprite = bladeShooter;
        projectileType = ProjectileTypes.Blade;
        projectilePierce += 1;
    }
    protected override void Path2toLv4()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (bloonInRange && isSearchCorutine == false)
        {
            StartCoroutine(TowerSearch());
            isSearchCorutine = true;
        }
        if (bloonInRange && isAttackCorutine == false && bloonInRangeIsSeeable)
        {
            StartCoroutine(TowerAttack());
            isAttackCorutine = true;
        }
    }

    protected override IEnumerator TowerSearch()
    {
        while (bloonInRange)
        {
            bloonInRangeIsSeeable = false;
            foreach (Collider2D bloonCollider in Physics2D.OverlapCircleAll(transform.position, range, BloonsMask, 0, 0))
            {
                if (bloonCollider.gameObject.TryGetComponent(out Bloon bloonObject))
                {
                    if (bloonObject.isCammo == false || canSeeCamo)
                    {
                        bloonInRangeIsSeeable = true;
                    }
                }
                else
                { Debug.LogError(bloonCollider.gameObject + " nie posiada komponentu Bloon"); }
            }
            yield return new WaitForSeconds(0.1f);
        }
        isSearchCorutine = false;
    }

    protected override IEnumerator TowerAttack()
    {
        while(bloonInRange && bloonInRangeIsSeeable)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                GameBox.instance.poolingMenager.SummonProjectile(projectileType, transform.position, projectilePierce, projectilePower, projectileSpeed, 360/projectileCount * i, range, false);
            }
            yield return new WaitForSeconds(attackSpeed);
        }
        isAttackCorutine = false;
    }
}
