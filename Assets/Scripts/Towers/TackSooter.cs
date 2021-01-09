using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackSooter : Tower
{
    [SerializeField]
    protected int projectileCount = 8;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (bloonInRange && isAttackCorutine == false)
        {
            StartCoroutine(TowerAttack());
            isAttackCorutine = true;
        }
    }

    protected override IEnumerator TowerAttack()
    {
        while(bloonInRange)
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
