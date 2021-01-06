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

    #region Uleprzenia
    [field: SerializeField]
    public int path1Lv { private set; get; } = 0;
    [field: SerializeField]
    public int path2Lv { private set; get; } = 0;
    #endregion

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

        //Obracanie samej wierzy w kierunku targetowanego bloona
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
            GameBox.instance.poolingMenager.SummonProjectile(projectileType, transform.position, projectilePierce, projectilePower, projectileSpeed, angle, range);
        }
        else
        {
            Debug.LogWarning("target is emppty");
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
        }
        else
        { Debug.LogError("niepoprawny nr pathu", gameObject); }
    }

    //poniższe do nadpisywania w dziedziceniach
    #region Upgrades Funcions
    protected virtual void Path1toLv1() { Debug.Log("P1 to Lv1"); path1Lv++; }
    protected virtual void Path1toLv2() { Debug.Log("P1 to Lv2"); path1Lv++; }
    protected virtual void Path1toLv3() { Debug.Log("P1 to Lv3"); path1Lv++; }
    protected virtual void Path1toLv4() { Debug.Log("P1 to Lv4"); path1Lv++; }
    protected virtual void Path2toLv1() { Debug.Log("P2 to Lv1"); path2Lv++; }
    protected virtual void Path2toLv2() { Debug.Log("P2 to Lv2"); path2Lv++; }
    protected virtual void Path2toLv3() { Debug.Log("P2 to Lv3"); path2Lv++; }
    protected virtual void Path2toLv4() { Debug.Log("P2 to Lv4"); path2Lv++; }
    #endregion
}
