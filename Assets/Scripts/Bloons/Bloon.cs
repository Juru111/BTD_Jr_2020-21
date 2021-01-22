using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    #region Dane potrzebne do spawnu kolejnych balownów
    [field: SerializeField]
    public BloonTypes bloonName { get; protected set; }
    [SerializeField]
    protected int layersLeft = 1;
    [field: SerializeField]
    public bool isCammo { protected set; get; }
    [SerializeField]
    protected float stunDurationLeft; 
    [field: SerializeField]
    public int myNextWaypoint { get; protected set; } = 0;
    [field: SerializeField]
    public float distanceToWaypoint { get; protected set; }
    [field: SerializeField]
    public List<GameObject> parentPopProjectles { get; protected set; }
    #endregion

    [SerializeField]
    protected float movementSpeed = 3.5f;
    protected float baseMovementSpeed;
    [SerializeField]
    protected UIMenager uiMenaner;
    [SerializeField]
    protected GameObject cammoSpriteObject;
    protected SpriteRenderer mySpriteRenderer;
    public bool neverLayerPoped { get; protected set; } = true;

    protected virtual void Awake()
    {
        uiMenaner = FindObjectOfType<UIMenager>();
        mySpriteRenderer = GetComponent<SpriteRenderer>(); //przydaje się też w Ceramic Bloon
        cammoSpriteObject.GetComponent<SpriteRenderer>().sortingOrder = mySpriteRenderer.sortingOrder + 1;
        baseMovementSpeed = movementSpeed;
        parentPopProjectles = new List<GameObject>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {

        //Gdy bloon dojdzie do kolejnego waypointa bierze kolejny
        distanceToWaypoint -= movementSpeed * Time.deltaTime;
        if (distanceToWaypoint <= 0)
        {
            myNextWaypoint++;
            if(myNextWaypoint <= GameBox.instance.waypoints.Length - 1)
            {
                ChangeWaypoint();
            }
            else
            {
                int hpToLose = 0;
                switch(bloonName)
                {
                    case BloonTypes.Red: hpToLose = 1; break;
                    case BloonTypes.Blue: hpToLose = 2; break;
                    case BloonTypes.Green: hpToLose = 3; break;
                    case BloonTypes.Yellow: hpToLose = 4; break;
                    case BloonTypes.Pink: hpToLose = 5; break;
                    case BloonTypes.Black: hpToLose = 11; break;
                    case BloonTypes.White: hpToLose = 11; break;
                    case BloonTypes.Lead: hpToLose = 23; break;
                    case BloonTypes.Zebra: hpToLose = 23; break;
                    case BloonTypes.Rainbow: hpToLose = 47; break;
                    case BloonTypes.Ceramic: hpToLose = 104; break;
                    case BloonTypes.MOAB: hpToLose = 616; break;
                    default: Debug.LogError("Niepoprawny bloon uciekł!"); break;
                }
                uiMenaner.LoseHp(hpToLose);

                GameBox.instance.poolingMenager.ReturnBloon(gameObject, bloonName);
                return;
            }
        }

        //Zmiana pozycji - ruch właściwy
        transform.position = Vector2.MoveTowards(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position, movementSpeed * Time.deltaTime);
    }

    protected virtual void ChangeWaypoint()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position);
    }

    public virtual void SetMe(BloonTypes _bloonName, int _layersLeft, Vector3 _position, int _myNextWayPoint,
                                float _distanceToWaypoint, bool _isCammo, float _stunDurationLeft, List<GameObject> _parentPopProjectles)
    {
        bloonName = _bloonName;
        layersLeft = _layersLeft;
        transform.position = _position;
        myNextWaypoint = _myNextWayPoint;
        distanceToWaypoint = _distanceToWaypoint;
        isCammo = _isCammo;
        cammoSpriteObject.SetActive(isCammo);
        parentPopProjectles = _parentPopProjectles;
        Debug.Log(_parentPopProjectles.Count);
        stunDurationLeft = _stunDurationLeft;
        movementSpeed = baseMovementSpeed;
        neverLayerPoped = true;
        if (stunDurationLeft > 0)
        {
            StartCoroutine(Stun());
        }


        if (layersLeft < (int)bloonName % 100)
        {
            LayerPop(0, null);
        }
    }

    public virtual void LayerPop(int power, GameObject parentPopProjectle)
    {
        if (neverLayerPoped)
        {
            neverLayerPoped = false;
            GameBox.instance.poolingMenager.SummonPop(transform.position);

            if (bloonName != BloonTypes.Red)
            {
                BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
                parentPopProjectles.Add(parentPopProjectle);
                GameBox.instance.poolingMenager.SummonBloon(newBloonName, layersLeft - power, transform.position,
                                                            myNextWaypoint, distanceToWaypoint, isCammo, stunDurationLeft, parentPopProjectles);
            }
            else
            {
                //Debug.Log("Red dead");
            }

            uiMenaner.ChangeMoneyBalance(1);
            GameBox.instance.poolingMenager.ReturnBloon(gameObject, bloonName);
        }
    }

    public virtual void StunMe(float _duration)
    {
        if(isStunCorutine == false)
        {
            stunDurationLeft = _duration;
            StartCoroutine(Stun());
        }
    }

    protected bool isStunCorutine;
    protected virtual IEnumerator Stun()
    {
        isStunCorutine = true;
        movementSpeed = baseMovementSpeed*0.001f;
        while (stunDurationLeft > 0)
        {
            stunDurationLeft -= Time.deltaTime;
            yield return null;
        }
        movementSpeed = baseMovementSpeed;
        isStunCorutine = false;
    }

}
