using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    #region Dane potrzebne do spawnu kolejnych balownów
    [SerializeField]
    protected BloonTypes bloonName;
    [SerializeField]
    protected int layersLeft = 1;
    [SerializeField]
    protected bool isCammo;
    [SerializeField]
    protected bool isRegrow;
    [SerializeField]
    protected int myNextWaypoint = 0;
    [SerializeField]
    protected float distanceToWaypoint;
    public GameObject parentPopProjectle { get; protected set; } = null;
    #endregion

    [SerializeField]
    protected float movementSpeed = 3.5f;
    [SerializeField]
    protected float rotationAngle;
    [SerializeField]
    protected GameObject popIcon;

    protected PoolingMenager poolingMenager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        poolingMenager = FindObjectOfType<PoolingMenager>();
        //transform.position = GameBox.instance.waypoints[0].transform.position;
        //myNextWaypoint = 1;
        //distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position);
        popIcon.SetActive(false);
    }

    // Update is called once per frame
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
            if(myNextWaypoint <= GameBox.instance.waypoints.Length-1)
            {
                distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position);
            }
            else
            {
                //gameObject.SetActive(false);
                //Desummon
                //HP-=LayesrLeft
                myNextWaypoint = 0;
                distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[0].transform.position);

            }
        }

        //Zmiana pozycji - ruch właściwy
        transform.position = Vector2.MoveTowards(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position, movementSpeed * Time.deltaTime);
    }

    public virtual void SetMe(BloonTypes _bloonName, int _layersLeft, Vector3 _position, int _myNextWayPoint,
                                float _distanceToWaypoint, bool _isCammo, bool _isRegrow, GameObject _parentPopProjectle)
    {
        bloonName = _bloonName;
        layersLeft = _layersLeft;
        transform.position = _position;
        myNextWaypoint = _myNextWayPoint;
        distanceToWaypoint = _distanceToWaypoint;
        isCammo = _isCammo;
        isRegrow = _isRegrow;
        parentPopProjectle = _parentPopProjectle;

        if(layersLeft < (int)bloonName)
        {
            LayerPop(0, parentPopProjectle);
        }
    }

    public virtual void LayerPop(int power, GameObject parentPopProjectle)
    {
        layersLeft -= power;
        StartCoroutine(ShowPoP());

        if(bloonName != BloonTypes.Red)
        {
            BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
            Debug.Log("summon-uję: " + newBloonName);
            poolingMenager.SummonBloon(newBloonName, layersLeft, transform.position, myNextWaypoint, distanceToWaypoint, isCammo, isRegrow, parentPopProjectle);
        }
        else
        { Debug.Log("Zabito Red-a"); }

        poolingMenager.ReturnBloon(gameObject, bloonName);
    }


    protected virtual IEnumerator ShowPoP()
    {
        popIcon.transform.Rotate(0f, 0f, Random.Range(0f, 360.0f));
        popIcon.SetActive(true);
        yield return new WaitForSeconds(0.11f);
        popIcon.SetActive(false);
    }

}
