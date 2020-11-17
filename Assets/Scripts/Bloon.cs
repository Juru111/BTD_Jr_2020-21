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
    protected Transform[] waypoints;
    [SerializeField]
    protected int myNextWaypiont = 1;
    [SerializeField]
    protected float distanceToWaypoint;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = waypoints[0].transform.position;
        distanceToWaypoint = Vector3.Distance(transform.position, waypoints[myNextWaypiont].transform.position);
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
            if(myNextWaypiont <= waypoints.Length-1)
            {
                distanceToWaypoint = Vector3.Distance(transform.position, waypoints[myNextWaypiont].transform.position);
            }
            else
            {
                //gameObject.SetActive(false);
                //HP-=LayesrLeft
                myNextWaypiont = 0;
                distanceToWaypoint = Vector3.Distance(transform.position, waypoints[0].transform.position);

            }
        }

        //Zmiana pozycji - ruch właściwy
        transform.position = Vector2.MoveTowards(transform.position, waypoints[myNextWaypiont].transform.position, movementSpeed * Time.deltaTime);
    }

    public virtual void LayerPop(int power)
    {
        layersLeft -= power;
        if (layersLeft <= 0)
        {
            Debug.Log("Bloon died");
            gameObject.SetActive(false);
        }
    }


}
