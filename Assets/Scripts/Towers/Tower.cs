using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float designRange;
    protected float range;
    [SerializeField]
    protected GameObject rangeIndicator;

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
        MultilpyRefreshRange(1);
        target = gameObject;
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
        //Obracanie samej wierzy
        if (bloonInRange)
        {
            transform.up = target.transform.position - transform.position;
        }
    }

    protected virtual void FixedUpdate()
    {
        //wartość ta jest nadpisywwana w OnTriggerStay gdyby balon był w zasięgu
        bloonInRange = false;
    }

    protected virtual void OnTriggerStay2D()
    {
        //zmiana na false jest w FixedUpdate
        bloonInRange = true;
    }

    protected virtual void MultilpyRefreshRange(float muntiplication)
    {
        designRange *= muntiplication;
        rangeIndicator.transform.localScale = new Vector3(designRange / 100, designRange / 100, 1);
        range = (float)(designRange * 0.0085); //zmienna "range" jest przydatna w innych miejscach też - jest bardziej naturalna dla Unity
        myCircleCollider2D.radius = range;
    }

    //Przeszukiwanie wszystkich balonów w zasięgu oraz stierdzenie, który jest najbliższy końca
    protected virtual IEnumerator TowerSearch()
    {
        int biggestNextWaypoint = -1;
        float leastDistanceToWaypoint = 999999;
        while (bloonInRange)
        {
            foreach (Collider2D bloonCollider in Physics2D.OverlapCircleAll(transform.position, range, BloonsMask, 0, 0))
            {
                //!!! Optymalizuj, ale jak?? : włąsny Collider2D, który zapisuje odrazu kalsy Bloon
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

    //atak wierzy wraz z przeliczeniem gdzie wysłać projectile oraz z cooldown-em
    protected virtual IEnumerator TowerAttack()
    {
        if (target != null)
        {
            //kalkulacje oraz summon projectile-a
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
