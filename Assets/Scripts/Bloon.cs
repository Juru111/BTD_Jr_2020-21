using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    [SerializeField]
    protected int layersLeft = 1;
    [SerializeField]
    protected float movementSpeed = 3.5f;
    [SerializeField]
    protected float rotationAngle;
    [SerializeField]
    protected bool isCammo;
    [SerializeField]
    protected bool isRegrow;
    [SerializeField]
    protected int myNextWaypiont = 0;
    [SerializeField]
    protected float distanceToWaypoint;
    [SerializeField]
    protected GameObject popIcon;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = GameBox.instance.waypoints[0].transform.position;
        myNextWaypiont = 1;
        distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypiont].transform.position);
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
            myNextWaypiont++;
            if(myNextWaypiont <= GameBox.instance.waypoints.Length-1)
            {
                distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypiont].transform.position);
            }
            else
            {
                //gameObject.SetActive(false);
                //HP-=LayesrLeft
                myNextWaypiont = 0;
                distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[0].transform.position);

            }
        }

        //Zmiana pozycji - ruch właściwy
        transform.position = Vector2.MoveTowards(transform.position, GameBox.instance.waypoints[myNextWaypiont].transform.position, movementSpeed * Time.deltaTime);
    }

    public virtual void LayerPop(int power)
    {
        layersLeft -= power;
        //Debug.Log("Balon " + this +" PoP-nięty z siłą " + power);
        StartCoroutine(ShowPoP());


        if (layersLeft <= 0)
        {
            //Debug.Log("Bloon " + this + "died");
            gameObject.SetActive(false);
        }
    }


    protected virtual IEnumerator ShowPoP()
    {
        popIcon.transform.Rotate(0f, 0f, Random.Range(0f, 360.0f));
        popIcon.SetActive(true);
        yield return new WaitForSeconds(0.11f);
        popIcon.SetActive(false);
    }

}
