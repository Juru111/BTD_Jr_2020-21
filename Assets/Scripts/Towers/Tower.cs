using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public TowerTypes towerName { private set; get; }
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float designTowerRange;
    [SerializeField]
    protected bool canSeeCamo;
    
    [SerializeField]
    protected ProjectileTypes projectileType;
    [SerializeField]
    protected int projectilePierce = 1;
    [SerializeField]
    protected int projectilePower = 1;
    [SerializeField]
    protected float projectileSpeed = 10;
    [SerializeField]
    protected GameObject rangeIndicator;


    protected float range;
    protected float projectileRange;
    protected CircleCollider2D myCircleCollider2D;
    protected SpriteRenderer mySpriteRenderer;
    protected UIMenager UIMenager;
    protected GameObject target;
    protected bool bloonInRange = false;
    protected bool bloonInRangeIsSeeable = false;
    protected bool isSearchCorutine = false;
    protected bool isAttackCorutine = false;
    protected LayerMask BloonsMask;

    public int path1Lv { protected set; get; } = 0;
    public int path2Lv { protected set; get; } = 0;

    protected virtual void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        BloonsMask = LayerMask.GetMask("Bloons");
        UIMenager = GameBox.instance.uIMenager;
        AddRefreshRange(0);
        target = gameObject;
    }


    protected virtual void Update()
    {
        if(bloonInRange && isSearchCorutine == false)
        {
            StartCoroutine(TowerSearch());
            isSearchCorutine = true;
        }
        if (bloonInRange && isAttackCorutine == false && bloonInRangeIsSeeable)
        {
            StartCoroutine(TowerAttack());
            isAttackCorutine = true;
        }

        //Obracanie samej wierzy w kierunku targetowanego bloona
        if (bloonInRange && bloonInRangeIsSeeable)
        {
            transform.up = transform.position - target.transform.position;
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

    protected virtual void AddRefreshRange(float additionDesignRange)
    {
        designTowerRange += additionDesignRange;
        rangeIndicator.transform.localScale = new Vector3(designTowerRange / 100, designTowerRange / 100, 1);
        range = (float)(designTowerRange * 0.0085); //zmienna "range" jest przydatna w innych miejscach też - jest bardziej naturalna dla Unity
        myCircleCollider2D.radius = range;

        projectileRange = range * 1.1f;
    }

    //Przeszukiwanie wszystkich balonów w zasięgu oraz stierdzenie, który jest najbliższy końca
    protected virtual IEnumerator TowerSearch()
    {
        int biggestNextWaypoint = -1;
        float leastDistanceToWaypoint = 999999;
        while (bloonInRange)
        {
            bloonInRangeIsSeeable = false;
            foreach (Collider2D bloonCollider in Physics2D.OverlapCircleAll(transform.position, range, BloonsMask, 0, 0))
            {
                //!!! Optymalizuj, ale jak?? : włąsny Collider2D, który zapisuje odrazu kalsy Bloon
                if (bloonCollider.gameObject.TryGetComponent(out Bloon bloonObject))
                {
                    if (bloonObject.isCammo == false || canSeeCamo)
                    {
                        bloonInRangeIsSeeable = true;
                        if (bloonObject.myNextWaypoint > biggestNextWaypoint)
                        {
                            biggestNextWaypoint = bloonObject.myNextWaypoint;
                            leastDistanceToWaypoint = bloonObject.distanceToWaypoint;
                            target = bloonObject.gameObject;
                        }
                        else if (bloonObject.myNextWaypoint == biggestNextWaypoint)
                        {
                            if (bloonObject.distanceToWaypoint < leastDistanceToWaypoint)
                            {
                                leastDistanceToWaypoint = bloonObject.distanceToWaypoint;
                                target = bloonObject.gameObject;
                            }
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
            GameBox.instance.poolingMenager.SummonProjectile(projectileType, transform.position, projectilePierce, projectilePower, projectileSpeed, angle, projectileRange, canSeeCamo);
        }
        else
        {
            Debug.LogError("target is emppty");
        }
        yield return new WaitForSeconds(attackSpeed);
        isAttackCorutine = false;
    }

    public virtual void UpgradeMe(int path)
    {
        if(path == 1)
        {
            switch (path1Lv)
            {
                case 0:
                    Path1toLv1();
                    break;
                case 1:
                    Path1toLv2();
                    break;
                case 2:
                    Path1toLv3();
                    break;
                case 3:
                    Path1toLv4();
                    break;
                default:
                    Debug.LogError("niepoprawny upgrade pathu 1", gameObject);
                    break;
            }
            path1Lv++;
        }
        else if (path == 2)
        {
            switch (path2Lv)
            {
                case 0:
                    Path2toLv1();
                    break;
                case 1:
                    Path2toLv2();
                    break;
                case 2:
                    Path2toLv3();
                    break;
                case 3:
                    Path2toLv4();
                    break;
                default:
                    Debug.LogError("niepoprawny upgrade pathu 2", gameObject);
                    break;
            }
            path2Lv++;
        }
        else
        { Debug.LogError("niepoprawny nr pathu", gameObject); }
    }

    //poniższe do nadpisywania w dziedziceniach
    #region Upgrades Funcions
    protected virtual void Path1toLv1() { }
    protected virtual void Path1toLv2() { }
    protected virtual void Path1toLv3() { }
    protected virtual void Path1toLv4() { }
    protected virtual void Path2toLv1() { }
    protected virtual void Path2toLv2() { }
    protected virtual void Path2toLv3() { }
    protected virtual void Path2toLv4() { }
    #endregion
}
