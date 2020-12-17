using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float range;
    protected CircleCollider2D myCircleCollider2D;
    [SerializeField]
    protected ProjectileTypes projectileType;
    [SerializeField]
    protected int projectilePierce = 1;
    [SerializeField]
    protected int projectilePower = 1;
    [SerializeField]
    protected float projectileSpeed = 10;
    
    protected GameObject target;
    [SerializeField]
    protected bool bloonInRange = false;
    [SerializeField]
    protected bool isSearchCorutine = false;
    [SerializeField]
    protected bool isAttackCorutine = false;
    LayerMask BloonsMask;

    protected virtual void Start()
    {
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        BloonsMask = LayerMask.GetMask("Bloons");
        myCircleCollider2D.radius = range;
    }


    protected virtual void Update()
    {
        if(bloonInRange && isSearchCorutine == false)
        {
            StartCoroutine(TowerSearch());
            isSearchCorutine = true;
        }
        if (bloonInRange && isAttackCorutine == false)
        {
            StartCoroutine(TowerAttack());
            isAttackCorutine = true;
        }
    }

    protected virtual void FixedUpdate()
    {
        bloonInRange = false;
    }

    protected virtual void OnTriggerStay2D()
    {
        bloonInRange = true;
    }

    //Przeszukiwanie wszystkich balonów w zasięgu oraz stierdzenie, który jest najbliższy końca
    protected virtual IEnumerator TowerSearch()
    {
        int biggestNextWaypoint = -1;
        float leastDistanceToWaypoint = 999999;
        while (bloonInRange)
        {
            Collider2D[] bloonColliders = Physics2D.OverlapCircleAll(transform.position, range, BloonsMask, 0, 0);
            foreach (var bloonCollider in bloonColliders)
            {
                //!!! Optymalizuj, ale jak?? : włąsny Collider2D, który zapisuje odrazy kalsy Bloon
                if (bloonCollider.gameObject.TryGetComponent(out Bloon bloonObject))
                {
                    if(bloonObject.myNextWaypoint > biggestNextWaypoint)
                    {
                        biggestNextWaypoint = bloonObject.myNextWaypoint;
                        leastDistanceToWaypoint = bloonObject.distanceToWaypoint;
                        target = bloonObject.gameObject;
                    }
                    else if (bloonObject.myNextWaypoint == biggestNextWaypoint)
                    {
                        if(bloonObject.distanceToWaypoint < leastDistanceToWaypoint)
                        {
                            leastDistanceToWaypoint = bloonObject.distanceToWaypoint;
                            target = bloonObject.gameObject;
                        }
                    }
                }
                else
                { Debug.LogError(bloonCollider.gameObject + " nie posiada komponentu Bloon"); }
            }
            yield return new WaitForSeconds(0.1f);
            biggestNextWaypoint = -1;
        }
        isSearchCorutine = false;
    }

    //atak wierzy wraz z przeliczeniem gdzie wysłać projectile oraz z cooldownem
    protected virtual IEnumerator TowerAttack()
    {
        if (target != null)
        {
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180 / Mathf.PI;
            GameBox.instance.PoolingMenager.SummonProjectile(projectileType, transform.position, projectilePierce, projectilePower, projectileSpeed, angle, range);
        }
        else
        {
            Debug.LogWarning("target is emppty");
        }
        yield return new WaitForSeconds(attackSpeed);
        isAttackCorutine = false;
    }

}
