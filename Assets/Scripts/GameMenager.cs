using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    private PoolingMenager poolingMenager;
    private Vector3 startPoint;
    private float startDistanceToWaypoint;
    

    void Start()
    {
        poolingMenager = FindObjectOfType<PoolingMenager>();
        startPoint = GameBox.instance.waypoints[0].position;
        startDistanceToWaypoint = Vector3.Distance(GameBox.instance.waypoints[0].transform.position, GameBox.instance.waypoints[1].transform.position);

        StartCoroutine(ConstantSpawn());
        
    }
    void Update()
    {
        
    }
    IEnumerator ConstantSpawn()
    {
        for (int i = 0; i < 20; i++)
        {
            poolingMenager.SummonBloon(BloonTypes.Red, 3, startPoint, 1, startDistanceToWaypoint, false, false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
